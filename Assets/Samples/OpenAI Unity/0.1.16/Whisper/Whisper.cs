using UnityEngine;
using UnityEngine.UI;

namespace OpenAI
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private Button recordButton;
        [SerializeField] private Image progressBar;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;
        
        private readonly string fileName = "output.wav";
        private int duration = 60;
        
        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi("YourApiKey", "YourOrganizationID");


        [SerializeField]
        GameObject GameplayLoopManagerGameobject;
        GameplayLoopManager gameplayLoopManager;

        [SerializeField]
        private Slider timerSlider;

        private float automaticStopTimer = 5.0f;
        private float playerPrefsTimer;
        private float beginningLeasureTimer = 2.0f;

        private void Start()
        {
            //ChangeMicrophone(0);

            duration = PlayerPrefs.GetInt("MicRecLength");
            if(PlayerPrefs.GetInt("ShowTimer") == 0)
            {
                timerSlider.gameObject.SetActive(false);
            }

            gameplayLoopManager = GameplayLoopManagerGameobject.GetComponent<GameplayLoopManager>();

            #if UNITY_WEBGL && !UNITY_EDITOR
            dropdown.options.Add(new Dropdown.OptionData("Microphone not supported on WebGL"));
            #else
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton.onClick.AddListener(StartRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);
            
            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);
            #endif
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }
        
        public void StartRecording()
        {
            isRecording = true;
            recordButton.enabled = false;
            playerPrefsTimer = PlayerPrefs.GetInt("MicRecLength");
            timerSlider.value = 1;

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            
            #if !UNITY_WEBGL
            clip = Microphone.Start(dropdown.options[index].text, false, duration, 44100);
            #endif
        }

        public async void EndRecording()
        {
            if (isRecording)//Damit wenn der Knopf doppelt gedrückt wird, es nicht 2 mal durchläuft und bricht
            {
                isRecording = false;
                message.text = "Transcripting...";

                playerPrefsTimer = PlayerPrefs.GetInt("MicRecLength");
                timerSlider.value = 1;

#if !UNITY_WEBGL
                Microphone.End(null);
#endif
                

                byte[] data = SaveWav.Save(fileName, clip);

                var req = new CreateAudioTranscriptionsRequest
                {
                    FileData = new FileData() { Data = data, Name = "audio.wav" },
                    //File = Application.persistentDataPath + "/" + fileName,
                    Model = "whisper-1",
                    Language = "de"
                };
                var res = await openai.CreateAudioTranscription(req);

                progressBar.fillAmount = 0;
                message.text = res.Text;

                gameplayLoopManager.Speech2TextMessage = message.text;
                gameplayLoopManager.GetLoudnessFromMicrophone(clip);
                gameplayLoopManager.EndLoop();

                recordButton.enabled = true;
            }
        }

        private void Update()
        {
            if (isRecording)
            {
                int dec = 28;
                float[] waveData = new float[dec];
                int micPos = Microphone.GetPosition(null) - (dec + 1);
                if (micPos < 0) return;
                clip.GetData(waveData, micPos);

                float levelMax = 0;
                for (int i = 0; i < dec; i++)
                {
                    float wavePeak = waveData[i] * waveData[i];
                    if (levelMax < wavePeak)
                    {
                        levelMax = wavePeak;
                    }
                }

                if(beginningLeasureTimer >= 0.0f)
                {
                    beginningLeasureTimer -= Time.deltaTime * 1;
                }
                
                if(levelMax <= 0.0002 && beginningLeasureTimer <= 0.0f)
                {
                    automaticStopTimer -= Time.deltaTime * 1;
                }
                else
                {
                    automaticStopTimer = 5.0f;
                }

                if (automaticStopTimer < 0.0f)
                {
                    EndRecording();
                }

                if(playerPrefsTimer >= 0.0f)
                {
                    playerPrefsTimer -= Time.deltaTime * 1;
                    timerSlider.value = Mathf.InverseLerp(0,PlayerPrefs.GetInt("MicRecLength"), playerPrefsTimer);
                }
                else
                {
                    EndRecording();
                }

            }




        }
    }
}

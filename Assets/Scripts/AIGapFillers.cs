using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGapFillers : MonoBehaviour
{


    public List<string> introductionGapFillers = new List<string> {"Vielen dank für die kurze Vorstellung.",
                                                            "Sehr gut, dann fahren wir mit dem Bewerbungsgespräch nun fort.",
                                                            "Vielen Dank." };
    public List<string> talkingGapFillers = new List<string> { "Verstehe.",
                                                        "Oookay.",
                                                        "Danke, für, die, Antwort.",
                                                        "Danke.",
                                                        "Vielen Dank.",
                                                        "Dankeschön für die Antwort.",
                                                        "Das notiere ich mir.",
                                                        "Vielen dank für ihre Ehrlichkeit.",
                                                        "Damit sind wir mit dem Thema durch.",
                                                        "Sehr gut.",
                                                        "Alles klar, verstehe.",
                                                        "Die Daten schreibe ich mir kurz auf." };
    public List<string> endingGapFillers = new List<string> { "Wir melden uns bei ihnen nach einer Entscheidung.",
                                                       "Auf wiederhören.",
                                                       "Sie bekommen Rückmeldung von uns." };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

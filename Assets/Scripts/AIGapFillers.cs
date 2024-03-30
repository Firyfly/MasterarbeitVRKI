using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Example Sentences used as Gap Fillers when the Ai is compiling
//-----------------------------------------

public class AIGapFillers : MonoBehaviour
{

    //Examples for the introduction
    public List<string> introductionGapFillers = new List<string> {"Vielen dank für die kurze Vorstellung.",
                                                            "Sehr gut, dann fahren wir mit dem Bewerbungsgespräch nun fort.",
                                                            "Vielen Dank." };
    //Examples for normal talking 
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
    //Examples for the Ending
    public List<string> endingGapFillers = new List<string> { "Wir melden uns bei ihnen nach einer Entscheidung.",
                                                       "Auf wiederhören.",
                                                       "Sie bekommen Rückmeldung von uns." };

}

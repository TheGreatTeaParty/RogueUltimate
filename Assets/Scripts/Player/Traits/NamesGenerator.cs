using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamesGenerator : MonoBehaviour
{
    private string[] Names = {
        "Matilda",
        "Alice",
        "Emma",
        "Eva",
        "Rose",
        "William",
        "John",
        "Robert",
        "Henry",
        "Ralph",
        "Thomas",
        "Walter",
        "Roger",
        "Hugh",

    };
   
    public string GetName()
    {
        return Names[Random.Range(0, Names.Length)];
    }
}

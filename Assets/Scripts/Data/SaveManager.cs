using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveManager
{
    public static void SavePlayer ()
    {
        BinaryFormatter formatter = new BinaryFormatter(); //Create a copy of BinaryFormatter
        string path = Application.persistentDataPath + "/player.dat"; //Make an adress of a our file
        FileStream stream = new FileStream(path, FileMode.Create); //Create our file with adress "path"

        PlayerData data = new PlayerData(); //Unity data -> general data for better saving
        
        formatter.Serialize(stream, data); //Transit data to a binary format, to the file "stream"
        stream.Close(); //For missing weird errors
    }

    public static void SaveAccount()
    {
        BinaryFormatter formatter = new BinaryFormatter(); //Create a copy of BinaryFormatter
        string path = Application.persistentDataPath + "/account.dat"; //Make an adress of a our file
        FileStream stream = new FileStream(path, FileMode.Create); //Create our file with adress "path"

        AccountData data = new AccountData(); //Unity data -> general data for better saving

        formatter.Serialize(stream, data); //Transit data to a binary format, to the file "stream"
        stream.Close(); //For missing weird errors
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.dat"; //Make an adress of a our file
        if (File.Exists(path)) //Check if saving exists
        {
            BinaryFormatter formatter = new BinaryFormatter(); //Create a copy of BinaryFormatter
            FileStream stream = new FileStream(path, FileMode.Open); //Open our file with adress "path" in veriable "stream"

            PlayerData data = formatter.Deserialize(stream) as PlayerData; //
            stream.Close(); //For missing weird errors

            return data;
        } else
        {
            //Debug.LogError("There is no save file in " + path);
            //Sets button resume non interactable
            return null;
        }
    }

    public static AccountData LoadAccount()
    {
        string path = Application.persistentDataPath + "/account.dat"; //Make an adress of a our file
        if (File.Exists(path)) //Check if saving exists
        {
            BinaryFormatter formatter = new BinaryFormatter(); //Create a copy of BinaryFormatter
            FileStream stream = new FileStream(path, FileMode.Open); //Open our file with adress "path" in veriable "stream"

            AccountData data = formatter.Deserialize(stream) as AccountData; //
            stream.Close(); //For missing weird errors

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeletePlayer()
    {
        string path = Application.persistentDataPath + "/player.dat";
        File.Delete(path);
        //AssetDatabase.Refresh();

    }

    public static void DeleteAccount()
    {
        string path = Application.persistentDataPath + "/account.dat";
        File.Delete(path);
    }

}

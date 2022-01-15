using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class Options {

    public static bool initiallyLoadedyet = false;


    public const string TWOSFLIPPPED = "TWOSFLIPPED", ORBSFLIPPPED = "ORBSFLIPPED", THREESFLIPPPED = "THREESFLIPPED", TOTALCOINS = "TOTALCOINS", HIGHSCORE = "HIGHSCORE";

    public static string SaveFolderLocation() {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.LinuxEditor) {
            return Application.dataPath + "/Save";
        } else {
            return Application.persistentDataPath + "/Save";
        }
    }


    public static Dictionary<string, bool> savedBools = new Dictionary<string, bool>();
    public static Dictionary<string, int> savedInts = new Dictionary<string, int>();
    public static Dictionary<string, string> savedStrings = new Dictionary<string, string>();
    public static Dictionary<string, float> savedFloats = new Dictionary<string, float>();

    public static bool GetBool(string key) {
        return savedBools[key];
    }

    public static int GetInt(string key) {
        return savedInts[key];
    }

    public static string GetString(string key) {
        return savedStrings[key];
    }

    public static bool HasBool(string key) {
        return savedBools.ContainsKey(key);
    }

    public static bool HasInt(string key) {
        return savedInts.ContainsKey(key);
    }

    public static bool HasString(string key) {
        return savedStrings.ContainsKey(key);
    }


    public static void SetBool(string key, bool b) {
        savedBools[key] = b;
    }

    public static void SetInt(string key, int i) {
        savedInts[key] = i;
    }

    public static void SetString(string key, string s) {
        savedStrings[key] = s;
    }

    public static float GetFloat(string key) {
        return savedFloats[key];
    }

    public static bool HasFloat(string key) {
        return savedFloats.ContainsKey(key);
    }



    public static float SetFloat(string key, float f) {
        return savedFloats[key] = f;
    }

    public static string GetOptionsFileLocation() {
        return SaveFolderLocation() + "/options.json";
    }

    public static void SaveOptions() {
        string savePath = GetOptionsFileLocation();
        if (!Directory.Exists(SaveFolderLocation())) Directory.CreateDirectory(SaveFolderLocation());
        OptionsSaveFile s = new OptionsSaveFile(savedFloats, savedInts, savedStrings, savedBools);
        File.WriteAllText(savePath, JsonTool.ObjectToString(s));
    }

    public static void LoadOptions() {
        if (!File.Exists(GetOptionsFileLocation())) {
            SaveOptions();
        }
        OptionsSaveFile s = JsonTool.StringToObject<OptionsSaveFile>(File.ReadAllText(GetOptionsFileLocation()));

        savedFloats = s.savedFloats;
        savedInts = s.savedInts;
        savedStrings = s.savedStrings;
        savedBools = s.savedBools;
        initiallyLoadedyet = true;
    }

    public static void AddInt(string key, int v) {
        savedInts[key] += v;
    }
}



[System.Serializable]
public class OptionsSaveFile {
    public Dictionary<string, float> savedFloats;
    public Dictionary<string, int> savedInts;
    public Dictionary<string, string> savedStrings;
    public Dictionary<string, bool> savedBools;

    public OptionsSaveFile(Dictionary<string, float> savedFloats, Dictionary<string, int> savedInts, Dictionary<string, string> savedStrings, Dictionary<string, bool> savedBools) {
        this.savedFloats = savedFloats;
        this.savedInts = savedInts;
        this.savedStrings = savedStrings;
        this.savedBools = savedBools;
    }
}

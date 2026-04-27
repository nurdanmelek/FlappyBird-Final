using System.Collections.Generic;
using UnityEngine;

public class WordsManager : MonoBehaviour
{
    public List<int> selectedStudyKeys = new List<int>();



    public List<int> completedKeys = new List<int>();
    
    public List<int> currentLevelKeys;
    
    public List<string> latinWords;
    public List<string> turkishWords;

    public void SetLevelKeys()
    {
        if (currentLevelKeys == null)
            currentLevelKeys = new List<int>();
        else
            currentLevelKeys.Clear();

        List<int> sourceKeys = new List<int>();

        if (selectedStudyKeys.Count > 0)
        {
            sourceKeys.AddRange(selectedStudyKeys);
        }
        else
        {
            for (int i = 0; i < latinWords.Count; i++)
            {
                sourceKeys.Add(i);
            }
        }

        List<int> availableKeys = new List<int>();

        foreach (int key in sourceKeys)
        {
            if (!completedKeys.Contains(key))
            {
                availableKeys.Add(key);
            }
        }

        if (availableKeys.Count < 3)
        {
            Debug.Log("Seçilen sözcüklerin tamamý oynandý.");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableKeys.Count);
            int selectedKey = availableKeys[randomIndex];

            currentLevelKeys.Add(selectedKey);
            availableKeys.RemoveAt(randomIndex);
        }
    }

    public void SetSelectedStudyKeys(List<int> selectedKeys)
    {
        selectedStudyKeys.Clear();
        completedKeys.Clear();

        foreach (int key in selectedKeys)
        {
            if (!selectedStudyKeys.Contains(key))
            {
                selectedStudyKeys.Add(key);
            }
        }
    }

    public List<string> ReturnLatinWords()
    {
        var newList = new List<string>();
        newList.Add(latinWords[currentLevelKeys[0]]);
        newList.Add(latinWords[currentLevelKeys[1]]);
        newList.Add(latinWords[currentLevelKeys[2]]);
        return newList;
    }

    public List<string> ReturnTurkishWords()
    {
        var newList = new List<string>();
        newList.Add(turkishWords[currentLevelKeys[0]]);
        newList.Add(turkishWords[currentLevelKeys[1]]);
        newList.Add(turkishWords[currentLevelKeys[2]]);
        return newList;
    }

    public void MarkCurrentWordsAsCompleted()
    {
        if (currentLevelKeys == null || currentLevelKeys.Count == 0)
            return;

        foreach (int key in currentLevelKeys)
        {
            if (!completedKeys.Contains(key))
            {
                completedKeys.Add(key);
            }
        }
    }

    public void SaveProgress()
    {
        string data = string.Join(",", completedKeys);
        PlayerPrefs.SetString("CompletedKeys", data);
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        completedKeys.Clear();

        string data = PlayerPrefs.GetString("CompletedKeys", "");

        if (string.IsNullOrEmpty(data)) return;

        string[] parts = data.Split(',');

        foreach (string part in parts)
        {
            if (int.TryParse(part, out int key))
            {
                completedKeys.Add(key);
            }
        }
    }

    public void ResetProgress()
    {
        completedKeys.Clear();

        PlayerPrefs.DeleteKey("CompletedKeys");
        PlayerPrefs.Save();
    }
}

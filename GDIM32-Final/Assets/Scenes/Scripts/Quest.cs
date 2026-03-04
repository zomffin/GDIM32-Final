
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string desciption;
    public List<QuestObjectives> objectives;


    private void OnValidate()     //Called when ScriptableObject is Edidted
    {
        if (string.IsNullOrEmpty(questID))
        {
            questID = questName + Guid.NewGuid().ToString();    //Gives a unique questID
        }

    }


    [System.Serializable] //Store data
    public class QuestObjectives
    {
        public string objectiveID;
        public string desciption;
        public ObjectiveType type;


        public int _requiredAmount;
        public int _currentAmount;

        public bool _isCompleted => _currentAmount >= _requiredAmount;
    }

    public enum ObjectiveType { CollectItems, TalkNPC }

    [System.Serializable]
    public class QuestProgress
    {
        public Quest quest;
        public List<QuestObjectives> objectives;

        public QuestProgress(Quest quest)
        {
            this.quest = quest;
            objectives = new List<QuestObjectives>();

        }


    }



}

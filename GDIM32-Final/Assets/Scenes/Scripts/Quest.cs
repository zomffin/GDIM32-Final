
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


    private void OnValidate() //Called when ScriptableObject is Edidted
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


    public int requiredAmount;
    public int currentAmount;

    public bool IsCompleted => currentAmount >= requiredAmount;
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

        //deep copy...not really sure about the Add function in tutorial 
        foreach (var obj in objectives)
        {
            objectives.Add(new QuestObjectives
            {
                objectiveID = obj.objectiveID,
                desciption = obj.desciption,
                type = obj.type,
                requiredAmount = obj.requiredAmount,
                currentAmount = 0

            });

        }

    }

    //
    public bool IsCompleted => objectives.TrueForAll(o => o.IsCompleted);
    public string QuestID => quest.questID;


}
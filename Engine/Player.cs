using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player : LivingCreature
    {
        public int Gold {  get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        public Location CurrentLocation { get; set; }

        public Player(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, int level) : base(currentHitPoints, maximumHitPoints)
        {
            Gold = gold;
            ExperiencePoints = experiencePoints;
            Level = level;
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.ItemRequiredtoEnter == null) return true;

            foreach(InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == location.ItemRequiredtoEnter.ID) return true;
            }

            return false;
        }

        public bool HasThisQuest(Quest quest)
        {
            foreach(PlayerQuest pq in Quests)
            {
                if (pq.Details.ID == quest.ID) return pq.IsCompleted;
            }

            return false;
        }

        public bool CompletedThisQuest(Quest quest)
        {
            foreach(PlayerQuest pq in Quests)
            {
                if (pq.Details.ID == quest.ID) return pq.IsCompleted;
            }
            return false;
        }

        public bool HasAllCompletionItems(Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                bool foundItemInPlayerInventory = false;

                foreach(InventoryItem ii in Inventory)
                {
                    if(ii.Details.ID == quest.ID)
                    {
                        foundItemInPlayerInventory = true;

                        if (ii.Quantity < qci.Quantity) return false;
                    }
                }

            if(!foundItemInPlayerInventory) return false;
            }

            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                foreach(InventoryItem ii in Inventory)
                {
                    if(ii.Details.ID == qci.Details.ID)
                    {
                        ii.Quantity -= qci.Quantity;
                        break;
                    }
                }
            }
        }

        public void AddItemToInventory(Item ita)
        {
            foreach(InventoryItem ii in Inventory)
            {
                if(ii.Details.ID == ita.ID)
                {
                    ii.Quantity++;
                    return;
                }
            }

            Inventory.Add(new InventoryItem(ita, 1));
        }

        public void MarkQuestCompleted(Quest quest)
        {
            foreach(PlayerQuest pq in Quests)
            {
                if(pq.Details.ID == quest.ID)
                {
                    pq.IsCompleted = true;
                    return;
                }
            }
        }
    }
}

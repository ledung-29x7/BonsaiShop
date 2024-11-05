﻿namespace BonsaiShop_API.Areas.Garden.Models
{
    public class Gardens
    {
        private int gadenId;
        private int gardenOwnerId;
        private string gardenName;
        private string address;
        private string phone;
        private string description;
        private DateTime createAt;

        public int GadenId { get => gadenId; set => gadenId = value; }
        public int GardenOwnerId { get => gardenOwnerId; set => gardenOwnerId = value; }
        public string GardenName { get => gardenName; set => gardenName = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Description { get => description; set => description = value; }
        public DateTime CreateAt { get => createAt; set => createAt = value; }
    }
}

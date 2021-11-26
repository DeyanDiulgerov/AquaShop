using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;

        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.Decorations = new List<IDecoration>();
            this.Fish = new List<IFish>();
        }
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                name = value;
            }
        }

        public int Capacity { get; }

        public int Comfort => this.Decorations.Sum(x => x.Comfort);


        public ICollection<IDecoration> Decorations { get; }

        public ICollection<IFish> Fish { get; }

        public void AddDecoration(IDecoration decoration)
        {
            this.Decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (this.Fish.Count == this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }
            this.Fish.Add(fish);
        }

        public void Feed()
        {
            foreach (IFish fish in this.Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            if (this.Fish.Any())
            {
                return $"{this.Name} ({this.GetType().Name}):\r\n" +
                       $"Fish: {string.Join(", ", this.Fish)}\r\n" +
                       $"Decorations: {this.Decorations.Count}\r\n" +
                       $"Comfort: { this.Comfort}";
            }
            else
            {
                return "none";
            }

            //return $"{this.Name} ({this.GetType().Name}):\r\n" +
            //       $"Fish: { this.Fish.Any() ? string.Join(", ", this.Fish.Select(x => x.Name)) : "none")}" +
            //       $"Decorations: { decorationsCount}\r\n" +
            //       $"Comfort: { aquariumComfort}\r\n";


        }

        public bool RemoveFish(IFish fish) => this.Fish.Remove(fish);
    }
}

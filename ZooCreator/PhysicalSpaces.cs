using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCreator
{
    class PhysicalSpaces
    {
        public PhysicalSpaces (string spaceID, int horizontalCoordinate, int verticalCoordinate, string occupyingAnimals = "", int animalQuantity = 0)
        {
            this.SpaceID = spaceID;
            this.HorizontalCoordinate = horizontalCoordinate;
            this.VerticalCoordinate = verticalCoordinate;
            this.OccupyingAnimals = occupyingAnimals;
            this.AnimalQuantity = animalQuantity;
        }

        private string spaceID;
        public string SpaceID
        {
            get { return spaceID; }
            private set
            {
                spaceID = value;
            }
        }

        public int AnimalQuantity
        { get; set; }
        
        public string OccupyingAnimals
        { get; set; }

        private int verticalCoordinate;
        private int horizontalCoordinate;

        public int VerticalCoordinate
        {
            get { return verticalCoordinate; }
            private set
            {
                verticalCoordinate = value;
            }
        }

        public int HorizontalCoordinate
        {
            get { return horizontalCoordinate; }
            private set
            {
                horizontalCoordinate = value;
            }
        }
    }
}

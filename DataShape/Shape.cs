using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataShape
{
    class Shape
    {
        #region Properties 
        string id;
        float width;
        float height;
        int sidesAmount;
        float[] position;
        float scale;
        float[] rotation;
        #endregion

        #region Accessors
        public string Id { get => id; set => id = value; }
        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }
        public int SidesAmount { get => sidesAmount; set => sidesAmount = value; }
        public float[] Position { get => position; set => position = value; }
        public float Scale { get => scale; set => scale = value; }
        public float[] Rotation { get => rotation; set => rotation = value; }
        #endregion

        #region Constructors
        public Shape()
        {

        }

        public Shape(float width, float height, int sidesAmount, float[] position, float scale, float[] rotation)
        {
            Id = IdGenerator();//fill the Id with the uniqe number
            Width = width;
            Height = height;
            SidesAmount = sidesAmount;
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }
        public Shape(string id, float width, float height, int sidesAmount, float[] position, float scale, float[] rotation)
        {
            Id = id;
            Width = width;
            Height = height;
            SidesAmount = sidesAmount;
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }
        #endregion

        #region Methods
        // create the unique id
        public string IdGenerator()
        {
            //the idea here is to use the time when we take an instance
            //so the id is the year /day of the year/hour/ minute/ secondes
            string uniqueId = DateTime.Now.Year.ToString()
                + IdPartsChecker(DateTime.Now.DayOfYear.ToString(), 3)
                + IdPartsChecker(DateTime.Now.Hour.ToString(),2)
                + IdPartsChecker(DateTime.Now.Minute.ToString(),2)
                + IdPartsChecker(DateTime.Now.Second.ToString(),2);
            return uniqueId;
        }

        //check every portion of Id if it's length is one
        //just to make sure that the id got every time the same number of character
        public string IdPartsChecker(string idSlice,int SliceLenght)
        {
            string correctFormat = "";
            if (SliceLenght == idSlice.Length)
                return idSlice;
            else if (SliceLenght - idSlice.Length == 1)
                correctFormat = "0" + idSlice;
            else if (SliceLenght - idSlice.Length == 2)
                correctFormat = "00" + idSlice;
            return correctFormat;
        }

        //Get shape Data
        public string ShowShape()
        {
            string shape = Id +
                 "    " + Width +
                 "         " + Height +
                 "         " + SidesAmount +
                 "              [" + Position[0] + ", " + Position[1] + ", " + Position[2] + "] " +
                 "   " + Scale +
                 "          [" + Rotation[0] + ", " + Rotation[1] + ", " + Rotation[2] + "] ";

            return shape;
        }
        //search shape Data
        public bool SearchShape(string idShape)
        {
            if (idShape == Id)
                return true;
            return false;
        }

        public void EditShapeObject(float[] position, float scale, float[] rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }
        #endregion
    }
}
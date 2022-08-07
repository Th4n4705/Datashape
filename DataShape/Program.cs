using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DataShape
{
    class Program
    {
        public static List<Shape> ShapeList = new List<Shape>();

        #region Structs
        public struct SPosition
        {
            public float x;
            public float y;
            public float z;
        }

        public struct SRotation
        {
            public float x;
            public float y;
            public float z;
        }

        public struct SShape
        {
            public float width;
            public float height;
            public int sidesAmount;
            public SPosition position;
            public float scale;
            public SRotation rotation;
        }
        #endregion

        static void Main(string[] args)
        {
            string filePath = @".\DataShapes.Json";
            if (File.Exists(filePath))
                LoadJson();
            //show the menu
            while (true)
            {
                ShowMenu();
            }
        }

        #region Load JSON FILE
        public static void LoadJson()
        {
            string filePath = @".\DataShapes.Json";
            if (!File.Exists(filePath))
                return;
            string jsonString = File.ReadAllText(filePath);
            ShapeList = JsonConvert.DeserializeObject<List<Shape>>(jsonString);
        }
        #endregion

        #region Menu list
        public static void ShowMenu()
        {

            int answer;
            Console.WriteLine("----------------------------------");
            Console.WriteLine("---WELCOME TO Shape APLICATION---");
            Console.WriteLine("----------------------------------");

            Console.WriteLine("1 - To add a shape Press 1");

            Console.WriteLine("2 - To remove a shape Press 2");

            Console.WriteLine("3 - To edit a shape Press 3");

            Console.WriteLine("4 - To search for a shape Press 4");

            Console.WriteLine("5 - To list all Shapes Press 5");

            Console.WriteLine("0 - To Exit the application Press 0");
            Console.WriteLine("----------------------------------");
            IntegerChecker(out answer);

            if (answer >= 0 && answer <= 5)
                switch (answer)
                {
                    case 0:
                        ExitApplication();
                        break;
                    case 1:
                        AddNewShape();
                        break;
                    case 2:
                        RemoveShape();
                        break;
                    case 3:
                        EditShape();
                        break;
                    case 4:
                        ListSearchedShape();
                        break;
                    case 5:
                        ListAllShapes();
                        break;
                }

        }
        #endregion

        #region Exit The Application
        public static void ExitApplication()
        {
            // Save The list in the file 
            File.WriteAllText("DataShapes.Json", JsonConvert.SerializeObject(ShapeList));
            //exit the application
            Environment.Exit(-1);
        }
        #endregion

        #region New Shape Creation
        public static void AddNewShape()
        {
            SShape sShape;

            Console.WriteLine("Width : ");
            FloatChecker(out sShape.width);

            Console.WriteLine("Height :");
            FloatChecker(out sShape.height);

            Console.WriteLine("Sides Amount : ");
            IntegerChecker(out sShape.sidesAmount);

            Console.WriteLine("Postion :");
            Console.WriteLine("X :");
            FloatChecker(out sShape.position.x);
            Console.WriteLine("Y :");
            FloatChecker(out sShape.position.y);
            Console.WriteLine("Z :");
            FloatChecker(out sShape.position.z);

            Console.WriteLine("Scale :");
            FloatChecker(out sShape.scale);

            Console.WriteLine("Rotation :");
            Console.WriteLine("X :");
            FloatChecker(out sShape.rotation.x);
            Console.WriteLine("Y :");
            FloatChecker(out sShape.rotation.y);
            Console.WriteLine("Z :");
            FloatChecker(out sShape.rotation.z);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You add a new shape successfully");
            Console.ResetColor();

            float[] position = { sShape.position.x, sShape.position.y, sShape.position.z };
            float[] rotation = { sShape.rotation.x, sShape.rotation.y, sShape.rotation.z };
            Shape newShape = new Shape(sShape.width, sShape.height, sShape.sidesAmount,position, sShape.scale,rotation);
            ShapeList.Add(newShape);
        }
        #endregion

        #region List All Shapes
        //listing all the shapes founded in the List<Shape> ShapeList
        public static void ListAllShapes()
        {
            Console.WriteLine("\nId:              Width:    Height:     Sides amount:  Postion:             Scale:       Rotation: ");
            foreach (Shape shape in ShapeList)
            {
                Console.WriteLine(shape.ShowShape());
            }
            Console.ReadLine();
        }
        #endregion

        #region Searching a specific shape
        //listing a specific shape founded in the List<Shape> ShapeList
        public static void ListSearchedShape()
        {
            Console.WriteLine("Type the Shape Id : ");
            string searchedId = Console.ReadLine();
            int founded = 0;
            foreach (Shape shape in ShapeList)
            {
                if (shape.SearchShape(searchedId))
                {
                    Console.WriteLine("\nId:              Width:    Height:     Sides amount:  Postion:             Scale:       Rotation: ");
                    Console.WriteLine(shape.ShowShape());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nThe shape has been found successfully");
                    Console.ResetColor();
                    founded = 1;
                    break;
                }
            }
            if (founded == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe shape not found!");
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion

        #region Edit a shape
        //edit a specific shape founded in the List<Shape> ShapeList
        public static void EditShape()
        {
            Console.WriteLine("Type the Shape Id : ");
            string searchedId = Console.ReadLine();
            int founded = 0;
            foreach (Shape shape in ShapeList)
            {
                if (shape.SearchShape(searchedId))
                {
                    SShape sShape = FillingTheNewData();
                    float[] position = { sShape.position.x, sShape.position.y, sShape.position.z };
                    float[] rotation = { sShape.rotation.x, sShape.rotation.y, sShape.rotation.z };
                    shape.EditShapeObject(position,sShape.scale,rotation);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The shape has been edited successfully");
                    Console.ResetColor();
                    founded = 1;
                    break;
                }
            }
            if (founded == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The shape not found!");
                Console.ResetColor();
            }
            Console.ReadLine();
        }

        //fill the founded object with the new data
        public static SShape FillingTheNewData()
        {
            SShape sShape;

            sShape.width = 0;
            sShape.height = 0;
            sShape.sidesAmount = 0;

            Console.WriteLine("Postion :");
            Console.WriteLine("X :");
            FloatChecker(out sShape.position.x);
            Console.WriteLine("Y :");
            FloatChecker(out sShape.position.y);
            Console.WriteLine("Z :");
            FloatChecker(out sShape.position.z);

            Console.WriteLine("Scale :");
            FloatChecker(out sShape.scale);

            Console.WriteLine("Rotation :");
            Console.WriteLine("X :");
            FloatChecker(out sShape.rotation.x);
            Console.WriteLine("Y :");
            FloatChecker(out sShape.rotation.y);
            Console.WriteLine("Z :");
            FloatChecker(out sShape.rotation.z);


            return sShape;
        }
        #endregion

        #region Remove a SHape
        //remove a shape after searching it's id
        public static void RemoveShape()
        {
            Console.WriteLine("Type the Shape Id : ");
            string searchedId = Console.ReadLine();
            int founded = 0;
            foreach (Shape shape in ShapeList)
            {
                if (shape.SearchShape(searchedId))
                {
                    ShapeList.Remove(shape);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The shape has been removed successfully");
                    Console.ResetColor();
                    founded = 1;
                    break;
                }
            }
            if (founded == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The shape not found!");
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion

        #region input Checkers
        // check the user input if float
        public static void FloatChecker(out float userInput)
        {
            string input = null;
            float inputFloat = 0;
            while (!float.TryParse(input, out inputFloat))
            {
                input = Console.ReadLine();
                if (!float.TryParse(input, out inputFloat))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("your input isn't a float try again");
                    Console.ResetColor();
                }
                else
                    break;
            }
            userInput = inputFloat;
        }

        // check the user input if float
        public static void IntegerChecker(out int userInput)
        {
            string input = null;
            int inputInteger = 0;
            while (!Int32.TryParse(input, out inputInteger))
            {
                input = Console.ReadLine();
                if (!Int32.TryParse(input, out inputInteger))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("your input isn't a integer try again");
                    Console.ResetColor();
                }
                else
                    break;
            }
            userInput = inputInteger;
        }
        #endregion
    }
}

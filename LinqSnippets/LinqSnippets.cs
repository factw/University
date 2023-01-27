using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class LinqSnippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat Leon"
            };

            // 1. Select * of cars

            var carList = from car in cars select car;

            foreach ( var car in carList ) 
            {
                Console.WriteLine(car);
            }

            //2. SELECT WHERE car is Audi (SELECT AUDIs)
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach(var audi in audiList)
            {
                Console.WriteLine(audi);
            }

        }

        public static void LinqNumbers()
        {
            List<int> numbers = new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9};
            var processedNumberList =
                numbers
                    .Select(num => num * 3)
                    .Where(num => num != 9)
                    .OrderBy(num => num);
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            var first = textList.First();

            var cText = textList.First(text => text.Equals("c"));

            var jText = textList.First(text => text.Contains("j"));

            var firsOrDefault = textList.FirstOrDefault(text => text.Contains("z"));

            var uniqueText = textList.Single();

            var uniqueDefault = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            var myEvenNumber = evenNumbers.Except(otherEvenNumbers);

        }

        static public void MultipleSelects()
        {
            string [] myOpinions =
            {
                "Opinion1, text1",
                "Opinion2, text2",
                "Opinion3, text3"
            };
            var myOpinionsSelection = myOpinions.SelectMany(opinion => opinion.Split(','));
        }
    }
}
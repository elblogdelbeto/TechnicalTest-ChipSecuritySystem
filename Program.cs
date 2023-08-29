using System;
using System.Collections.Generic;

namespace ChipSecuritySystem
{
    class Program
    {
        static List<ColorChip> FindMaxLinkedChips(Dictionary<Color, List<ColorChip>> chipDictionary, Color currentColor)
        {
            List<ColorChip> maxChips = new List<ColorChip>();

            if (!chipDictionary.ContainsKey(currentColor))
                return maxChips;

            foreach (var chip in chipDictionary[currentColor])
            {
                Dictionary<Color, List<ColorChip>> newChipDictionary = new Dictionary<Color, List<ColorChip>>(chipDictionary);
                newChipDictionary[currentColor] = newChipDictionary[currentColor].FindAll(c => c != chip);

                List<ColorChip> linkedChips = FindMaxLinkedChips(newChipDictionary, chip.EndColor);
                linkedChips.Insert(0, chip);

                if (linkedChips.Count > maxChips.Count)
                    maxChips = linkedChips;
            }

            return maxChips;
        }



        static bool CanLinkChips(List<ColorChip> chips)
        {
            Dictionary<Color, List<ColorChip>> chipDictionary = new Dictionary<Color, List<ColorChip>>();
            foreach (var chip in chips)
            {
                if (!chipDictionary.ContainsKey(chip.StartColor))
                    chipDictionary[chip.StartColor] = new List<ColorChip>();
                chipDictionary[chip.StartColor].Add(chip);
            }

            List<ColorChip> maxLinkedChips = FindMaxLinkedChips(chipDictionary, Color.Blue);

            if (maxLinkedChips.Count > 0 && maxLinkedChips[maxLinkedChips.Count - 1].EndColor == Color.Green)
            {               
                foreach (var chip in maxLinkedChips)
                {
                    Console.WriteLine(chip.ToString());
                }
                return true;
            }
            else
                return false;
        }



        static void Main(string[] args)
        {
            List<ColorChip> chips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Purple, Color.Red)
            };

            if (CanLinkChips(chips))
                Console.WriteLine("Chips can be linked from end to end");
            else
                Console.WriteLine("Chips cannot be linked from end to end");

            Console.ReadLine();
        }
    }
}

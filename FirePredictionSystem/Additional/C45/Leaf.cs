﻿using System.Linq;
using System.Collections.Generic;

namespace FirePredictionSystem.Additional.C45
{
    public class Leaf
    {
        public List<Leaf> Children { get; set; }
        public Classifier Classifier { get; set; }
        public Node       Parent    { get; set; }
        public Input      Input { get; set; }

        public bool IsAnswer { get; set; }

        public Leaf()
        {
            Children = new List<Leaf>();
        }

        public Leaf(Input input)
        {
            Children = new List<Leaf>();
            Input = new Input();
            Input.Data = input.Data;
        }

        public void SetChildren()
        {
            SetClassifier();

            foreach (Node node in Classifier.Nodes)
            {
                Input subset = Input.GetSubset(Classifier.AttributeName,
                                                 node.Name);
                
                Leaf childLeaf = new Leaf(subset);
                childLeaf.Parent = node; 

                if (node != null && node.Entropy != 0)
                {
                    childLeaf.IsAnswer = false;
                }
                else if ((node != null && node.Entropy == 0) ||
                        (Classifier.GainRatio == System.Double.NaN))
                {
                    childLeaf.IsAnswer = true;
                }
                    
                Children.Add(childLeaf);

            }
        }

        public void SetClassifier()
        {
            
            string[][] data = Input.Data;
            List<Classifier> ClassifierCollection = new List<Classifier>();

            string[] answers = Input.
                    GetAttributeColumn(data[0].Length - 1);

            //changed to 0 (было->1)
            for (int j = 1; j < data[0].Length - 1; j++)
            {
                string[] attributeColumn = Input.GetAttributeColumn(j);
                Classifier classifier = 
                    new Classifier(attributeColumn,
                                   answers);

                ClassifierCollection.Add(classifier);
            }

            ClassifierCollection.ForEach(x => x.BuildNodes());
            ClassifierCollection.Sort();
            Classifier = ClassifierCollection.Last();
            
        }

    }
}

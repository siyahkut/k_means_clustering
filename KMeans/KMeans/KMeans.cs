using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KMeans
{
    class KMeans
    {
        Bitmap mainBitmap;
        GVector[,] inputGVector;
        public Bitmap output;
        private int cluster;
        public KMeans(Bitmap inputB,int inputCluster)
        {
            this.mainBitmap = inputB;
           this.inputGVector = ConvertToGVector(inputB);
            this.cluster = inputCluster;
        }


        public GVector[,] ConvertToGVector(Bitmap input)
        {
            
            Bitmap bt = (Bitmap)input.Clone();
            GVector[,] result = new GVector[bt.Width, bt.Height];
            for (int y = 0; y < bt.Height; y++)
            {
                for (int x = 0; x < bt.Width; x++)
                {
                    Color c = bt.GetPixel(x, y);
                    result[x, y] = new GVector(c.R, c.G, c.B,x,y);
                    
                }
            }
            return result;
        }
        public  Bitmap CalculateKmeans(int ClusterCount, GVector[,] input, int iterations)
        {
            int length = input.GetLength(0);
            int height = input.GetLength(1);

            Random randomizer = new Random();

            List<GVector> centroides = new List<GVector>(ClusterCount);
            List<List<GVector>> clusters = new List<List<GVector>>(ClusterCount);

            for (int i = 0; i < ClusterCount; i++)
            {
                GVector centroid = input[randomizer.Next(0, length), randomizer.Next(0, height)];

                List<GVector> cluster = new List<GVector>();

                cluster.Add(centroid);
                clusters.Add(cluster);

                centroides.Add(centroid);
            }

            int count = 0;

            while (count < iterations)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                {
                    for (int y = 0; y < input.GetLength(1); y++)
                    {
                        GVector currentVector = input[x, y];
                        double min_distance = Double.PositiveInfinity;
                        GVector closest_centroid = centroides[0];

        
                        foreach (GVector centroid in centroides)
                        {
                            double distance = currentVector.Distance(centroid);
                            if (Math.Pow(distance, 2) < min_distance)
                            {
                                min_distance = Math.Pow(distance, 2);
                                closest_centroid = centroid;
                            }
                        }

             
                        List<GVector> to_assign = clusters[0];
                        foreach (List<GVector> cluster_list in clusters)
                        {
                            if (cluster_list[0].r == closest_centroid.r && cluster_list[0].g == closest_centroid.g && cluster_list[0].b == closest_centroid.b)
                            {
                                to_assign = cluster_list;
                                break;
                            }
                        }

                        to_assign.Add(currentVector);
                        int current_length = to_assign.Count;
                        GVector current_centroid = to_assign[0];

                        GVector new_centroid = new GVector(0, 0, 0);

                        foreach (GVector vector in to_assign)
                        {
                            new_centroid = new_centroid.Sum(vector);
                        }

                        new_centroid = new GVector(new_centroid.r / current_length, new_centroid.g / current_length, new_centroid.b / current_length);
                        to_assign.RemoveAt(0);
                        to_assign.Insert(0, new_centroid);

                        for (int i = 0; i < centroides.Count; i++)
                        {
                            if (centroides[i].r == current_centroid.r && centroides[i].g == current_centroid.g && current_centroid.b == centroides[i].b)
                            {
                                centroides[i] = new_centroid;
                                break;
                            }
                        }
                    }
                }
                count++;
            }

            foreach (List<GVector> cluster_list in clusters)
            {
                GVector current_centroid = cluster_list[0];
                for (int i = 1; i < cluster_list.Count; i++)
                {
                    cluster_list[i].r = current_centroid.r;
                    cluster_list[i].g = current_centroid.g;
                    cluster_list[i].b = current_centroid.b;
                }
            }

            Bitmap output = (Bitmap)this.mainBitmap.Clone(); 

            foreach (List<GVector> cluster_list in clusters)
            {
                for (int i = 0; i < cluster_list.Count; i++)
                {
                    GVector current_vector = cluster_list[i];
                    output.SetPixel(current_vector.x, current_vector.y,Color.FromArgb((int)current_vector.r, (int)current_vector.g, (int)current_vector.b));
                    //output[current_vector.x, current_vector.y] = Color.FromArgb((int)current_vector.r, (int)current_vector.g, (int)current_vector.b);
                }
            }

            return output;
        }

        private void ApplyKMeans(int iteration)
        {
            this.output = CalculateKmeans(this.cluster, this.inputGVector,  iteration);

        }


        public void Run(int iteration)
        {
            this.ApplyKMeans(iteration);
        }

    }
}


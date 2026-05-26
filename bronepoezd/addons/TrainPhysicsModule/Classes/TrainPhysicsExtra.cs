using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

    public static class TrainPhysicsExtra
    {
        /// <summary>
        /// Рекурсивно возвращает всех детей в принципе в виде списка
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static List<Node> GetAllKids(Godot.Collections.Array<Node> nodes, int DepthLimit = 100)
        {
            List<Node> Result = new List<Node>();
            foreach (Node item in nodes.ToList<Node>())
            {
                Result.Add(item);
                if (item.GetChildCount() > 0 && DepthLimit > 0)
                {
                    Result.AddRange(GetAllKids(item.GetChildren(true),DepthLimit-1).ToList<Node>());
                }
            }
            return Result;
        }

    }
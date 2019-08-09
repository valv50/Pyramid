using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pyramid
{
    public class PyramidProcessing
    {
        private bool _isEven;

        private int[][] _initialData;

        private List<(int sum, int lastIndex, int lastLevel, List<(int index, int value)> path)> _results;          

        public PyramidProcessing(int[][] initialData)
        {
            _initialData = initialData;

            _results = new List<(int, int, int, List<(int index, int value)>)>();
        }

        public (int sum, List<(int index, int value)> path) Process()
        {
            for (int i1 = 1; i1 <= _initialData.Length; i1++)
            {
                if (i1 == 1)
                {
                    _isEven = _initialData[i1 - 1][0] % 2 == 0;

                    FillFirstLevel();
                }
                else
                {
                    FillNewLevel(i1, _isEven);
                }

                _isEven = !_isEven;
            }

            (int sum, List<(int index, int value)> path) result;

            result = _results.OrderByDescending(o => o.sum)
                .Select(s => (s.sum, s.path))
                .First();

            return result;
        }

        private void FillFirstLevel()
        {
            (int sum, int lastIndex, int lasLevel, List<(int index, int value)> path) resultItem;

            resultItem = (_initialData[0][0]
                , 1
                , 1
                , new List<(int index, int value)>());

            resultItem.path.Add((1, _initialData[0][0]));

            _results.Add(resultItem);
        }

        private void FillNewLevel(int level, bool isEven)
        {
            List<(int sum, int lastIndex, int lastLevel, List<(int index, int value)> path)> newItems;

            newItems = new List<(int sum, int lastIndex, int lastLevel, List<(int index, int value)> path)>();

            foreach ((int sum, int lastIndex, int lastLevel, List<(int index, int value)> path) resultItem in _results)
            {
                for (int i = 0; i <= 1; i++)
                {
                    int index = resultItem.lastIndex + i;

                    if ((_initialData[level - 1][index - 1] % 2 == 0) == isEven)
                    {
                        newItems.Add(CopyItem(resultItem, index, _initialData[level - 1][index - 1]));
                    }
                }                
            }

            _results.RemoveAll(r => r.lastLevel < level);

            var highestInGroups =
                from n in newItems
                group n by n.lastIndex into itemgrp
                let topSum = itemgrp.Max(x => x.sum)
                select new
                {
                    lastIndex = itemgrp.Key,
                    maxSum = topSum
                };
            
            _results.AddRange(
                from newItem in newItems
                join highestInGroup in highestInGroups on
                    new { newItem.lastIndex, newItem.sum } 
                    equals new { highestInGroup.lastIndex, sum = highestInGroup.maxSum}
                select newItem);
        }

        private (int sum, int lastIndex, int lastLevel, List<(int index, int value)> path) 
            CopyItem((int sum, int lastIndex, int lastLevel, List<(int index, int value)> path) resultItem
            , int index, int value)
        {
            (int sum, int lastIndex, int lastLevel, List<(int index, int value)> path) newItem;

            List<(int index, int value)> newPath = new List<(int index, int value)>();

            newPath.AddRange(resultItem.path);

            newPath.Add((index, value));

            newItem = (value + resultItem.sum
                , index
                , resultItem.lastLevel + 1
                , newPath);

            return newItem;

        }
    }
}

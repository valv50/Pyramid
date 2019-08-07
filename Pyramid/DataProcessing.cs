using System;
using System.Collections.Generic;
using System.Text;

namespace Pyramid
{
    public class PyramidProcessing
    {
        private bool _isEven;

        private int[][] _initialData;

        private List<(int sum, List<(int index, int value)>)> _results;          

        PyramidProcessing(int[][] initialData)
        {
            _initialData = initialData;

            _results = new List<(int sum, List<(int index, int value)>)>();
        }

        public void Process()
        {
            for (int i1 = 1; i1 <= _initialData.Length; i1++)
            {
                if (i1 == 1)
                {
                    _isEven = _initialData[i1 - 1][0] % 2 == 0;
                }

                for (int i2 = 1; i2 <= _initialData[i1 - 1].Length)
                {
                    if (_initialData[i1 - 1][i2 - 1] % 2 ==
                        (_isEven ? 0 : 1))
                    {

                    }

                }

                _isEven = !_isEven;
            }
        }
    }
}

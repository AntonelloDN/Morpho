using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Morpho25.Utility;
using MorphoGeometry;

namespace MorphoReader
{
    public class BuildingOutput : BinaryOutput
    {
        public BuildingOutput(string edx)
            : base(edx)
        {
            _offset = 4 * _numX * _numY * _numZ;
            _buffer = 4 * _numX * _numY * _numZ * 3;

            BasePoint = new Vector(0, 0, 0);
        }

        public BuildingOutput(string edx, Vector basePoint) 
            : this(edx)
        {
            BasePoint = basePoint;
        }

        public override void SetValuesFromBinary(string edt, List<Facade> facades, int variable)
        {
            using (FileStream SourceStream = File.Open(edt, FileMode.Open))
            {
                BinaryReader binReader = new BinaryReader(SourceStream);

                binReader.BaseStream.Position = _buffer * variable + _offset;
                byte[] dateArray = binReader.ReadBytes(_buffer);

                /*
                 *    |----|----|----|
                 */

                int facadeLength = 12;

                for (int f = 0; f < facades.Count; f++)
                {
                    int count = 0;
                    int start = f * facadeLength;

                    for (int i = start; i < start + facadeLength; i += 4)
                    {
                        float number = BitConverter.ToSingle(dateArray, i);
                        if (count == 0)
                            facades[f].ValueX = number;
                        else if (count == 1)
                            facades[f].ValueY = number;
                        else
                            facades[f].ValueZ = number;

                        count++;

                        if (count == 3)
                            count = 0;
                    }

                }
            }
        }
    }
}

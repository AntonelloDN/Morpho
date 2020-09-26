using MorphoGeometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphoReader
{
    public class GridOutput : BinaryOutput
    {
        public GridOutput(string edx) 
            : base(edx)
        {
            _offset = _buffer = 4 * _numX * _numY * _numZ;

            BasePoint = new Vector(0, 0, 0);
        }

        public GridOutput(string edx, Vector basePoint) 
            : this(edx)
        {
            BasePoint = basePoint;
        }

        public override void SetValuesFromBinary(string edt, List<Facade> facades, int variable)
        {
            using (FileStream SourceStream = File.Open(edt, FileMode.Open))
            {
                BinaryReader binReader = new BinaryReader(SourceStream);

                binReader.BaseStream.Position = _buffer * variable;
                byte[] dateArray = binReader.ReadBytes(_buffer);

                /*
                 *    |----|
                 */

                int count = 0;
                float number;
                for (int i = 0; i < dateArray.Length; i += 4)
                {
                    number = BitConverter.ToSingle(dateArray, i);
                    facades[count].ValueZ = number;
                    count++;
                }
            }
        }
    }
}

using MorphoGeometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphoReader
{
    /// <summary>
    /// Grid output of envimet.
    /// </summary>
    public class GridOutput : BinaryOutput
    {
        /// <summary>
        /// Create a new grid output object.
        /// </summary>
        /// <param name="edx">EDX file.</param>
        public GridOutput(string edx) 
            : base(edx)
        {
            _offset = _buffer = 4 * _numX * _numY * _numZ;

            BasePoint = new Vector(0, 0, 0);
        }

        /// <summary>
        /// Create a new grid output object.
        /// </summary>
        /// <param name="edx">EDX file.</param>
        /// <param name="basePoint">Base point.</param>
        public GridOutput(string edx, Vector basePoint) 
            : this(edx)
        {
            BasePoint = basePoint;
        }

        /// <summary>
        /// Set values from a binary file.
        /// </summary>
        /// <param name="edt">EDT file.</param>
        /// <param name="facades">Facades to map.</param>
        /// <param name="variable">Variable to read.</param>
        public override void SetValuesFromBinary(string edt, 
            List<Facade> facades, int variable)
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

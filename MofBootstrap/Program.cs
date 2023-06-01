using MetaDslx.Modeling;
using MofBootstrapLib.Generator;
using MofBootstrapLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MofBootstrap
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmiSerializer = new MofXmiSerializer();

            ImmutableModel mofModel = xmiSerializer.ReadModelFromFile("../../../MOF.xmi");

            MofGenerator mofGenerator = new MofGenerator(mofModel, MofToGenerate.CMOF);

            mofGenerator.Generate("mof.mm");
        }
    }
}

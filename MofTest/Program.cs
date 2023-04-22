using MetaDslx.Modeling;
using MofImplementationLib.Generator;
using MofImplementationLib.Model;
using System;
using System.IO;

namespace MofTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //MofTestConstantsGen.TestUmlMethods();
            Test001();
        }

        static void Test001()
        {
            //var xmiSerializer = new MofXmiSerializer();
            //var model = xmiSerializer.ReadModelFromFile("../../../MOF.xmi");
            //var generator = new MofModelToMetaModelGenerator(model.Objects);
            //var generatedCode = generator.Generate("MofImplementationLib.Model", "Mof", "http://www.omg.org/spec/MOF");
            //var expectedCode = File.ReadAllText("../../../../MofImplementationLib/Model/Mof.mm");
            //File.WriteAllText("../../../../MofImplementationLib/Model/MofTestGen.mm", generatedCode);
            //Console.WriteLine(generatedCode == expectedCode); // This should print "true"

            MutableModel model = new MutableModel("TestModel");
            MofFactory mofFactory = new MofFactory(model);

            TypeBuilder stringType = mofFactory.Type();
            TypeBuilder intType = mofFactory.Type();

            ClassBuilder class0 = mofFactory.Class();
            class0.Name = "Allat";
            PropertyBuilder class0Property = mofFactory.Property();
            class0Property.Name = "Faj";
            class0Property.Type = stringType;
            class0.OwnedAttribute.Add(class0Property);

            // class1
            var class1 = mofFactory.Class();
            class1.Name = "Celeb";
            PropertyBuilder class1Property1 = mofFactory.Property();
            PropertyBuilder class1Property2 = mofFactory.Property();

            OperationBuilder class1operation1 = mofFactory.Operation();

            class1operation1.Name = "ChangeNev";

            ParameterBuilder nameParam = mofFactory.Parameter();
            nameParam.Name = "ujNev";
            nameParam.Type = stringType;
            class1operation1.OwnedParameter.Add(nameParam);

            stringType.Name = "string";
            intType.Name = "int";
            class1Property1.Type = stringType;
            class1Property1.Name = "Nev";
            class1Property2.Type = intType;
            class1Property2.Name = "Eletkor";

            class1.OwnedAttribute.Add(class1Property1);
            class1.OwnedAttribute.Add(class1Property2);
            class1.OwnedOperation.Add(class1operation1);

            GeneralizationBuilder general1 = mofFactory.Generalization();
            general1.General = class0;

            class1.Generalization.Add(general1);

            // class2
            var class2 = mofFactory.Class();
            class2.Name = "HaziAllat";
            PropertyBuilder class2Property1 = mofFactory.Property();
            PropertyBuilder class2Property2 = mofFactory.Property();
            class2Property1.Type = stringType;
            class2Property2.Type = stringType;
            class2Property1.Name = "Nev";
            class2Property2.Name = "BundaSzin";

            class2.OwnedAttribute.Add(class2Property1);
            class2.OwnedAttribute.Add(class2Property2);

            GeneralizationBuilder general2 = mofFactory.Generalization();
            general2.General = class0;
            class2.Generalization.Add(general2);

            PackageBuilder package = mofFactory.Package();
            package.Name = "MOF_modell_teszt";

            package.PackagedElement.Add(class0);
            package.PackagedElement.Add(class1);
            package.PackagedElement.Add(class2);


            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            var generator = new MofModelToMetaModelGenerator(package.ToImmutable().PackagedElement);
            var generatedCode = generator.Generate("SampleNamespace", "MOF_modell_teszt", "http://example.org/mytesztlang/0.1");
            File.WriteAllText("ModellTeszt.mm", generatedCode);
        }
    }
}

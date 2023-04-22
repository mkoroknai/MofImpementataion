using System;
using System.Collections.Generic;
using System.Text;
using MetaDslx.Modeling;
using MofBootstrapLib.Model;

namespace MofBootstrap
{
    class ClassClassElement
    {
        readonly string ClassContentName;
        readonly string ClassName;
        readonly string TypeName;

        public ClassClassElement(string className, string elementName, string typeName)
        {
            ClassName = className;
            ClassContentName = elementName;
            TypeName = typeName;
        }

        public override string ToString()
        {
            string elemString = ClassName + "::" + ClassContentName;

            string spaces = "";
            int n = 55 - elemString.Length;

            for (int i = 0; i < n; i++) spaces += " ";
                
            elemString += spaces + "(type not found: " + TypeName + ")";

            return elemString;
        }
    }

    class ClassClassElementList
    {

        List<ClassClassElement> list = new List<ClassClassElement>();

        public void Add(ClassClassElement element)
        {
            list.Add(element);
        }

        public override string ToString()
        {
            string listString = "";

            foreach(ClassClassElement c in list)
            {
                listString += c.ToString() + Environment.NewLine;
            }

            return listString;
        }
    }
}

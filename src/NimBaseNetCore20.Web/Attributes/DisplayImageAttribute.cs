using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NimBaseNetCore20.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class DisplayImageAttribute : Attribute
    {
        public readonly string DisplayImage;
        public DisplayImageAttribute(string image)  //image from http://fontawesome.io/icons/ to display
        {
            this.DisplayImage = image;
        }
    }
}

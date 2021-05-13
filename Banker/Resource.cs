using System;
namespace Banker
{
    public class Resource
    {
        private int available;

        public int Available
        {
            set { available = value; }
            get { return available; }
        }
        public Resource()
        {
        }
        public Resource(int avl)
        {
            this.available = avl;
        }
    }
}

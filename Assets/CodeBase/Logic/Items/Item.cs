﻿namespace CodeBase.Logic.Items
{
    public abstract class Item
    {
        public string Name { get; }

        public Item(string name) =>
            Name = name;
    }
}
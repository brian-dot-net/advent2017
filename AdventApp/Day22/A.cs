﻿namespace Advent.Day22
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Grid(input).Run(10000);
    }
}
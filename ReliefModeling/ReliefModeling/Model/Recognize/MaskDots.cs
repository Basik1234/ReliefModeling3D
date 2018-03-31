using System;

namespace ReliefModeling.Model.Recognize
{
    public static class MaskDots
    {
        public enum Masks
        {
            Empty,
            DiagonalLeft,    //слева направо
            DotDownLeft,
            DotDownRight,
            DiagonalRight,   //справа налево
            Ceiling,
            AngleUpLeft,
            AngleUpRight,
            Floor,
            WallLeft,
            DotUpRight,
            DotUpLeft,
            WallRight,
            Full,
            AngleDownLeft,
            AngleDownRight
        }

        public static Masks GetMask(bool[,] dots)
        {
            if (dots[0, 0] == false && dots[0, 1] == false && dots[1, 0] == false && dots[1, 1] == false)
                return Masks.Empty;
            if (dots[0, 0] == true && dots[0, 1] == false && dots[1, 0] == false && dots[1, 1] == true)
                return Masks.DiagonalLeft;
            if (dots[0, 0] == false && dots[0, 1] == false && dots[1, 0] == true && dots[1, 1] == false)
                return Masks.DotDownLeft;
            if (dots[0, 0] == false && dots[0, 1] == false && dots[1, 0] == false && dots[1, 1] == true)
                return Masks.DotDownRight;
            if (dots[0, 0] == false && dots[0, 1] == true && dots[1, 0] == true && dots[1, 1] == false)
                return Masks.DiagonalRight;
            if (dots[0, 0] == true && dots[0, 1] == true && dots[1, 0] == false && dots[1, 1] == false)
                return Masks.Ceiling;
            if (dots[0, 0] == true && dots[0, 1] == true && dots[1, 0] == true && dots[1, 1] == false)
                return Masks.AngleUpLeft;
            if (dots[0, 0] == true && dots[0, 1] == true && dots[1, 0] == false && dots[1, 1] == true)
                return Masks.AngleUpRight;
            if (dots[0, 0] == false && dots[0, 1] == false && dots[1, 0] == true && dots[1, 1] == true)
                return Masks.Floor;
            if (dots[0, 0] == true && dots[0, 1] == false && dots[1, 0] == true && dots[1, 1] == false)
                return Masks.WallLeft;
            if (dots[0, 0] == false && dots[0, 1] == true && dots[1, 0] == false && dots[1, 1] == false)
                return Masks.DotUpRight;
            if (dots[0, 0] == true && dots[0, 1] == false && dots[1, 0] == false && dots[1, 1] == false)
                return Masks.DotUpLeft;
            if (dots[0, 0] == false && dots[0, 1] == true && dots[1, 0] == false && dots[1, 1] == true)
                return Masks.WallRight;
            if (dots[0, 0] == true && dots[0, 1] == true && dots[1, 0] == true && dots[1, 1] == true)
                return Masks.Full;
            if (dots[0, 0] == true && dots[0, 1] == false && dots[1, 0] == true && dots[1, 1] == true)
                return Masks.AngleDownLeft;
            if (dots[0, 0] == false && dots[0, 1] == true && dots[1, 0] == true && dots[1, 1] == true)
                return Masks.AngleDownRight;
            
            throw new Exception("Несуществующий TemplateDots");
        }
    }
}
using System.Collections.Generic;

namespace ReliefModeling.Model.Recognize
{
    public interface IFinderIsolines
    {
        List<Isoline> Find();
    }
}
using System.Collections;
using System.Collections.Generic;

    public interface ICell
    {
        Entity CurrentEntity { get; set; }
        bool IsEmpty { get; }
        void Clear();
    }

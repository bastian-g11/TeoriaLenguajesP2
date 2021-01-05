using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomataController : MonoBehaviour
{
    public int index;
    public AutomataType nextAutomata;
    public string exp = "";

    #region Automatas
    MainStructure mc;
    ReservedWord rw;
    RWVariableSyntaxis rwvs;
    RWVariableSyntaxisII rwvs2;
    DTVariableSyntax dtvs;
    DTCVariableSyntax dtcvs;
    VariableSyntax vs;
    StackAutomata sa;

    #endregion

    #region singleton
    public static AutomataController instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    void Start()
    {
        mc = new MainStructure();
        rw = new ReservedWord();
        rwvs = new RWVariableSyntaxis();
        rwvs2 = new RWVariableSyntaxisII();
        dtvs = new DTVariableSyntax();
        dtcvs = new DTCVariableSyntax();
        vs = new VariableSyntax();
        sa = new StackAutomata();
        nextAutomata = AutomataType.MainStructure;
        index = 0;
    }

    public AutomataType StartMainStructure(string lineToRead, int _index)
    {
        return mc.ReadStructure(lineToRead, _index);
    }

    public AutomataType StartReserverdWord(string lineToRead, int _index)
    {
        return rw.FindReservedWord(lineToRead, _index);
    }

    public AutomataType StartRWVariableSyntax(string lineToRead, int _index)
    {
        return rwvs.FindReservedWordInVariable(lineToRead, _index);
    }

    public AutomataType StartRWVariableSyntaxII(string lineToRead, int _index)
    {
        return rwvs2.FindReservedWordInVariable(lineToRead, _index);
    }

    public AutomataType StartDTVariableSyntax(string lineToRead, int _index)
    {
        return dtvs.CheckDataTypeVariableSyntax(lineToRead, _index);
    }

    public AutomataType StartDTCVariableSyntax(string lineToRead, int _index)
    {
        return dtcvs.CheckDataTypeVariableSyntax(lineToRead, _index);
    }

    public AutomataType StartVariableSyntax(string lineToRead, int _index)
    {
        return vs.CheckVariableSyntax(lineToRead, _index);
    }
    public AutomataType StartStackAutomata(string lineToRead, int _index)
    {
        return sa.CheckRightSideStructure(lineToRead, _index);
    }
}

using System;
using Microsoft.Office.Interop.Visio;

namespace HtmlFormData
{
    class UndoUnit : IVBUndoUnit
    {
        private bool _done;
        private readonly Action _doAction;
        private readonly Action _undoAction;

        public UndoUnit(Action doAction, Action undoAction)
        {
            _doAction = doAction;
            _undoAction = undoAction;
        }

        public int UnitSize => 50;

        public void Do(IVBUndoManager undoManager)
        {
            try
            {
                if (_done)
                {
                    _undoAction();
                }
                else
                {
                    _doAction();
                }

                _done = !_done;

                undoManager?.Add(this);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.Write(err.ToString());
            }
        }

        public void OnNextAdd()
        {
        }

        public string Description => "Update shape properties";

        public string UnitTypeCLSID => String.Empty;

        public int UnitTypeLong => 0;
    }
}
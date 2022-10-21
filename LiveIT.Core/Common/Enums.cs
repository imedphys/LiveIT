using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LiveIT.Common
{


    public enum PalletStatus
    {
        [Description("Αναμονή για φόρτωση χαρτοκιβωτίων")]
        PalletBoxLoading = 0,
        [Description("Δημιουργήθηκε")]
        Created,
        [Description("Προστέθηκε στην αποθήκη")]
        AddedToWarehouse,
        [Description("Επιλέχθηκε για φόρτωση")]
        SelectedForLoad,
        [Description("Επιβεβαίωθηκε για φόρτωση")]
        ConfirmedForLoad,
        [Description("Φορτώθηκε")]
        Loaded,
        [Description("Άκυρο")]
        Cancel = -1,
    }
}

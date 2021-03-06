﻿CREATE INDEX [IX_TableThree_Col3_Col4_b]
    ON [dbo].[TableThree]([Col3] ASC, [Col4] ASC)
    INCLUDE([Col2]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


CREATE ASSEMBLY [AdamNachman.SqlClr]
    AUTHORIZATION [dbo]
    FROM 0x4D5A90000300000004000000FFFF0000B800000000000000400000000000000000000000000000000000000000000000000000000000000000000000800000000E1FBA0E00B409CD21B8014CCD21546869732070726F6772616D2063616E6E6F742062652072756E20696E20444F53206D6F64652E0D0D0A2400000000000000504500004C010300E78D824C0000000000000000E00002210B0108000012000000060000000000001E300000002000000040000000004000002000000002000004000000000000000400000000000000008000000002000000000000030040850000100000100000000010000010000000000000100000000000000000000000C42F00005700000000400000B803000000000000000000000000000000000000006000000C00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000200000080000000000000000000000082000004800000000000000000000002E7465787400000024100000002000000012000000020000000000000000000000000000200000602E72737263000000B8030000004000000004000000140000000000000000000000000000400000402E72656C6F6300000C000000006000000002000000180000000000000000000000000000400000420000000000000000000000000000000000300000000000004800000002000500A0220000240D00000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000133002004800000001000011281900000A6F1A00000A027B030000043315027B020000041FFE330B02167D02000004020A2B0716730B0000060A06027B050000047D0400000406027B070000047D06000004062A1E0228040000062A1B3003001E01000002000011027B020000040B07450300000005000000F8000000CD00000038F300000002157D02000004027C04000004FE16030000016F1B00000A6F1C00000A281D00000A3ACC000000027B060000046F1E00000A3ABC000000027B060000046F1F00000A176A3FAA00000002027C04000004282000000A027B060000046F2100000A6F2200000A7D0800000402177D0200000402027B080000047D0B00000402167D0C0000042B5702027B0B000004027B0C0000049A7D0A000004027B0A000004027C09000004282300000A2C2302027B090000048C1D0000017D0100000402187D02000004170ADE3602177D0200000402027B0C00000417587D0C000004027B0C000004027B0B0000048E69329902280C000006160ADE07022809000006DC062A0000411C00000400000000000000150100001501000007000000000000001E027B010000042A1A732400000A7A00133002001F00000003000011027B020000040A061759450200000001000000010000002A02280C0000062A1E027B010000042A7A02282500000A02037D0200000402281900000A6F1A00000A7D030000042A2202157D020000042A001330020018000000010000111FFE730B0000060A06027D0500000406037D07000004062A4A0302A51D000001282600000A81050000012A1E02282500000A2A0042534A4201000100000000000C00000076322E302E35303732370000000005006C0000006C040000237E0000D80400007806000023537472696E677300000000500B00000800000023555300580B0000100000002347554944000000680B0000BC01000023426C6F6200000000000000020000015717A20B0902000000FA253300160000010000001F000000030000000C0000000C0000000500000005000000270000001500000003000000010000000200000002000000070000000200000001000000020000000100000000000A0001000000000006004D0046000600670054000A0094007F000A009E007F000A00AE007F000A00E600CB0006002401050106004301310106005A0131010600770131010600960131010600AF0131010600C80131010600E30131010600FE01310106001702050106002B02310106004402460006007A025A0206009A025A020A00CA02CB0006000703EC0206001503EC0206002303540006002F03460006008A0577050600B305A2050600E905460006002906460006003806460006005A065A0200000000010000000000010001000100100020002A0005000100010003011000DF0200000500010004000100C303C90001006C04D50001007704D5000600C600D8000600B704D8000600F800DC000600C104DC000600CC04E0000600D904D5000600E204E4000600FC04E00006000705D5006022000000009600A7000A0001008422000000009600B700130003009722000000008618C0001B000500502000000000E1013B03AF000500A42000000000E1018D03C0000500AC2000000000E101BA03C5000500F42100000000E109D003CC000500FC2100000000E1011E041B000500042200000000E10149041B0005002F2200000000E1098C04CC0005003722000000008618C0004F0005005622000000008100EE041B00060000000100C60000000200F80000000100FE00020002000201000001006C04030006000300090003000A0003006100030065003100C0001B003900C0001B004100C00045004900C00045005100C00045005900C00045006100C00045006900C00045007100C00045007900C00045008100C0004A008900C00045009100C0004A009900C0004F00A100C0001B00A900C0001B000C007F03B70011007F03C000C100BA03C50014001204D000C10043041B00C90064041B00C1001204CC00D100C0001B00D900BA05F000D900CC05F5000900E005FE00E100F005FE00E100F505020121000306C50021000E06070119001906FE00210019060B01E10023061001E9002F061701F100C0001B000900C0001B0029004E062701F900C0001B0020008300540024000B001F002E001B002D012E00230044012E002B0044012E007B009A012E003B005C012E00430067012E00730091012E0033004A012E004B0044012E005B0044012E006B008B0144000B00320063003B01EB008000C300EB00A000C300EB00E000C300EB000001C300EB004001C300EB006001C300EB00F9001E0123010300010000001205E70000005005E70002000700030002000A00050003000800230003000A00250003000C00270003000E002900030010002B00030012002D00030014002F00A300A90004800000010005003B0F038F000000000000B802000002000000000000000000000001003D00000000000200000000000000000000000100730000000000030002000000003C4D6F64756C653E004164616D4E6163686D616E2E5371436C722E646C6C0053656C656374696F6E004164616D4E6163686D616E2E53716C436C72006D73636F726C69620053797374656D004F626A6563740053797374656D2E436F6C6C656374696F6E730049456E756D657261626C650053797374656D2E446174610053797374656D2E446174612E53716C54797065730053716C537472696E670053716C4368617273004765744964730053716C496E7433320046696C6C526F7773002E63746F72006C697374004D6963726F736F66742E53716C5365727665722E5365727665720053716C46616365744174747269627574650064656C696D006F626A0069640053797374656D2E52756E74696D652E496E7465726F705365727669636573004F75744174747269627574650053797374656D2E5265666C656374696F6E00417373656D626C795469746C6541747472696275746500417373656D626C794465736372697074696F6E41747472696275746500417373656D626C79436F6E66696775726174696F6E41747472696275746500417373656D626C79436F6D70616E7941747472696275746500417373656D626C7950726F6475637441747472696275746500417373656D626C79436F7079726967687441747472696275746500417373656D626C7954726164656D61726B41747472696275746500417373656D626C7943756C7475726541747472696275746500436F6D56697369626C6541747472696275746500417373656D626C7956657273696F6E41747472696275746500434C53436F6D706C69616E744174747269627574650053797374656D2E52756E74696D652E436F6D70696C6572536572766963657300436F6D70696C6174696F6E52656C61786174696F6E734174747269627574650052756E74696D65436F6D7061746962696C697479417474726962757465004164616D4E6163686D616E2E5371436C720053716C46756E6374696F6E417474726962757465003C4765744964733E645F5F300053797374656D2E436F6C6C656374696F6E732E47656E657269630049456E756D657261626C6560310049456E756D657261746F7260310049456E756D657261746F720049446973706F7361626C650053797374656D2E436F6C6C656374696F6E732E47656E657269632E49456E756D657261626C653C53797374656D2E4F626A6563743E2E476574456E756D657261746F7200476574456E756D657261746F720053797374656D2E436F6C6C656374696F6E732E49456E756D657261626C652E476574456E756D657261746F72004D6F76654E657874003C3E325F5F63757272656E740053797374656D2E436F6C6C656374696F6E732E47656E657269632E49456E756D657261746F723C53797374656D2E4F626A6563743E2E6765745F43757272656E74006765745F43757272656E740053797374656D2E436F6C6C656374696F6E732E49456E756D657261746F722E52657365740052657365740053797374656D2E49446973706F7361626C652E446973706F736500446973706F7365003C3E315F5F7374617465003C3E6C5F5F696E697469616C54687265616449640053797374656D2E436F6C6C656374696F6E732E49456E756D657261746F722E6765745F43757272656E74003C3E335F5F6C697374003C3E335F5F64656C696D003C76616C7565733E355F5F31003C69643E355F5F32003C76616C75653E355F5F33003C3E6D5F5F46696E616C6C7934003C3E375F5F7772617035003C3E375F5F77726170360053797374656D2E436F6C6C656374696F6E732E47656E657269632E49456E756D657261746F723C53797374656D2E4F626A6563743E2E43757272656E740053797374656D2E436F6C6C656374696F6E732E49456E756D657261746F722E43757272656E740053797374656D2E446961676E6F737469637300446562756767657248696464656E4174747269627574650053797374656D2E546872656164696E6700546872656164006765745F43757272656E74546872656164006765745F4D616E61676564546872656164496400546F537472696E6700537472696E67005472696D0049734E756C6C4F72456D707479006765745F49734E756C6C006765745F4C656E677468006765745F56616C75650053706C697400496E743332005472795061727365004E6F74537570706F72746564457863657074696F6E006F705F496D706C6963697400436F6D70696C657247656E657261746564417474726962757465000000000003200000000000DCF8589298B74E4FA446E5873E04A2130008B77A5C561934E0890800021209110D1211070002011C1011150320000112010001005408074D617853697A65FFFFFFFF12010001005408074D617853697A6501000000042001010E042001010204200101084E0100030054020F497344657465726D696E697374696301540E1146696C6C526F774D6574686F644E616D650846696C6C526F7773540E0F5461626C65446566696E6974696F6E07496420696E742005151259011C0515125D011C07200015125D011C08200015125D01130004200012610320000202061C0320001C04200013000206080306110D0306121103061D0E02060E0328001C0401000000040000126D03200008040701120C0320000E040001020E0320000A0420001D030620011D0E1D03060002020E1008040702020803070108050001111508160100114164616D4E6163686D616E2E5371436C7200000501000000001101000C4164616D204E6163686D616E00000A0100055371436C7200002301001E436F7079726967687420C2A9204164616D204E6163686D616E203230313000000501000100000801000800000000001E01000100540216577261704E6F6E457863657074696F6E5468726F777301000000EC2F000000000000000000000E300000002000000000000000000000000000000000000000000000003000000000000000000000000000000000000000005F436F72446C6C4D61696E006D73636F7265652E646C6C0000000000FF25002040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001001000000018000080000000000000000000000000000001000100000030000080000000000000000000000000000001000000000048000000584000005C03000000000000000000005C0334000000560053005F00560045005200530049004F004E005F0049004E0046004F0000000000BD04EFFE0000010005000100038F3B0F05000100038F3B0F3F000000000000000400000002000000000000000000000000000000440000000100560061007200460069006C00650049006E0066006F00000000002400040000005400720061006E0073006C006100740069006F006E00000000000000B004BC020000010053007400720069006E006700460069006C00650049006E0066006F0000009802000001003000300030003000300034006200300000003C000D00010043006F006D00700061006E0079004E0061006D006500000000004100640061006D0020004E006100630068006D0061006E00000000004C0012000100460069006C0065004400650073006300720069007000740069006F006E00000000004100640061006D004E006100630068006D0061006E002E005300710043006C007200000040000F000100460069006C006500560065007200730069006F006E000000000031002E0035002E0033003800390039002E0033003600360031003100000000004C001600010049006E007400650072006E0061006C004E0061006D00650000004100640061006D004E006100630068006D0061006E002E005300710043006C0072002E0064006C006C00000060001E0001004C006500670061006C0043006F007000790072006900670068007400000043006F0070007900720069006700680074002000A90020004100640061006D0020004E006100630068006D0061006E002000320030003100300000005400160001004F0072006900670069006E0061006C00460069006C0065006E0061006D00650000004100640061006D004E006100630068006D0061006E002E005300710043006C0072002E0064006C006C0000002C0006000100500072006F0064007500630074004E0061006D006500000000005300710043006C007200000044000F000100500072006F006400750063007400560065007200730069006F006E00000031002E0035002E0033003800390039002E00330036003600310031000000000048000F00010041007300730065006D0062006C0079002000560065007200730069006F006E00000031002E0035002E0033003800390039002E00330036003600310031000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000003000000C000000203000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
    WITH PERMISSION_SET = SAFE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba.CsConsoleFormat;
using static System.ConsoleColor;
using static Alba.CsConsoleFormat.GridLength;

namespace game
{
    public class TableGenerator
    {
        public static void PrintsTable(string[] moves)
        {
            var doc = new Document();
            doc.Children.Add(CreatesTable(moves));
            ConsoleRenderer.RenderDocument(doc);
        }

        private static Grid CreatesTable(string [] moves)
        {
            var table = new Grid { Stroke = LineThickness.Double, Color = White };
            AddColumns(table, moves);
            FillsInTable(table, moves);
            return table;
        }

        private static void FillsInTable(Grid table,string[] moves)
        {
            FillsInHeader(table, moves);
            for (int i = 0; i < moves.Length; i++)
                FillsInLine(table, moves,i);    
        }

        private static void FillsInLine(Grid table,string [] moves,int i)
        {
            table.Children.Add(CreatesCell(moves[i],Align.Left));
            table.Children.Add(moves.Select((m, e)
                => CreatesCell(WinnersDaterminant.DaterminesWinner(moves, e, i))));
        }

        private static void FillsInHeader (Grid table,string[] moves)
        {
            table.Children.Add(CreatesCellWithoutStroke("  \\ User",Align.Left));
            table.Children.Add(moves.Select(m => CreatesCellWithoutStroke("")));
            table.Children.Add(CreatesCellWithoutStroke("PC \\",Align.Left));
            table.Children.Add(moves.Select(m => CreatesCellWithoutStroke(m)));
        }

        private static Cell CreatesCell(string str,Align align = Align.Center)
        {
            var cell = new Cell { Align = align };
            cell.Children.Add(str);
            return cell;
        }

        private static Cell CreatesCellWithoutStroke(string str,Align align=Align.Center)
        {
            var stroke = new LineThickness(LineWidth.Single ,LineWidth.None);
            var cell = new Cell { Stroke=stroke, Align=align};
            cell.Children.Add(str);
            return cell;
        }

        private static void AddColumns(Grid table,string [] moves)
        {
            var maxLengthStr = moves.Max(m => m.Length)+2;
            table.Columns.Add(new Column { Width = maxLengthStr>9?Char(maxLengthStr):Char(9) });
            table.Columns.Add(moves.Select(m => new Column { Width = m.Length>4?Char(m.Length + 2):Char(6) }));
        }
    }
}

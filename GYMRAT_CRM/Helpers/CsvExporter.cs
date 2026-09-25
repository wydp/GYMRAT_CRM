using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CRM.winforms.Helpers
{
    // Writes a DataGridView's contents to a CSV file and lets the user
    // pick where to save it.
    public static class CsvExporter
    {
        public static void ExportGrid(DataGridView grid, string suggestedFileName)
        {
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show("There's nothing to export — the report is empty.",
                    "Nothing to Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                FileName = suggestedFileName,
                DefaultExt = "csv",
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();

                // Header row
                for (int c = 0; c < grid.Columns.Count; c++)
                {
                    if (c > 0) sb.Append(',');
                    sb.Append(EscapeCsv(grid.Columns[c].HeaderText));
                }
                sb.AppendLine();

                // Data rows
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;

                    for (int c = 0; c < grid.Columns.Count; c++)
                    {
                        if (c > 0) sb.Append(',');
                        var value = row.Cells[c].Value?.ToString() ?? "";
                        sb.Append(EscapeCsv(value));
                    }
                    sb.AppendLine();
                }

                File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show($"Exported to:\n{dialog.FileName}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not export: {ex.Message}", "Export Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string EscapeCsv(string value)
        {
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }
    }
}

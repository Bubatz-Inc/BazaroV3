using Blazorise.Charts;

namespace Bazaro.Web.Pages.Main
{
    public partial class Dashboard
    {
        // Tree View
        public class Item
        {
            public string Text { get; set; }
            public IEnumerable<Item> Children { get; set; }
        }

        private IEnumerable<Item> Items = new[]
        {
            new Item { Text = "Folder 1" },
            new Item {
            Text = "Folder 2",
            Children = new []
            {
                new Item { Text = "Note 1" },
                new Item { Text = "Subfolder 1", Children = new []
                {
                    new Item { Text = "Note 1" },
                    new Item { Text = "Note 2" },
                    new Item { Text = "Note 3" },
                    new Item { Text = "Note 4" }
                }
            },
                new Item { Text = "Subfolder 2" },
                new Item { Text = "Subfolder 3" }
            }
        },
            new Item { Text = "Folder 3" },
            new Item { Text = "Folder 4" },
            new Item { Text = "Folder 5" },
            new Item { Text = "Folder 6" },
            new Item { Text = "Folder 7" },
            new Item { Text = "Folder 8" },
        };

        private IList<Item> ExpandedNodes = new List<Item>();
        private Item selectedNode;

        // Chart
        private LineChart<double> lineChart;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await HandleRedraw();
            }
        }

        private async Task HandleRedraw()
        {
            await lineChart.Clear();

            await lineChart.AddLabelsDatasetsAndUpdate(Labels, GetLineChartDataset());
        }

        private LineChartDataset<double> GetLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = "# of Notes",
                Data = RandomizeData(),
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                Fill = true,
                PointRadius = 2,
                BorderDash = new List<int> { }
            };
        }

        private string[] Labels = { "June", "Juli", "September", "Oktober", "November", "December" };
        private List<string> backgroundColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
        private List<string> borderColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

        private List<double> RandomizeData()
        {
            var r = new Random(DateTime.Now.Millisecond);

            return new List<double> { r.Next(3, 50) * r.NextDouble(), r.Next(3, 50) * r.NextDouble(), r.Next(3, 50) * r.NextDouble(), r.Next(3, 50) * r.NextDouble(), r.Next(3, 50) * r.NextDouble(), r.Next(3, 50) * r.NextDouble() };
        }
    }
}
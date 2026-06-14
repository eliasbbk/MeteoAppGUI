namespace MeteoAppGUI;

public partial class Form1 : Form
{
    private TextBox txtVille;
    private Button btnRechercher;
    private Label lblResultat;

    public Form1()
    {
        InitializeComponent();

        this.Text = "App Météo";
        this.Width = 400;
        this.Height = 300;

        txtVille = new TextBox();
        txtVille.Location = new Point(20, 20);
        txtVille.Width = 200;
        this.Controls.Add(txtVille);

        btnRechercher = new Button();
        btnRechercher.Text = "Rechercher";
        btnRechercher.Location = new Point(230, 20);
        this.Controls.Add(btnRechercher);

        lblResultat = new Label();
        lblResultat.Location = new Point(20, 60);
        lblResultat.Width = 350;
        lblResultat.Height = 150;
        this.Controls.Add(lblResultat);

        btnRechercher.Click += BtnRechercher_Click;
    }

    private async void BtnRechercher_Click(object? sender, EventArgs e)
{
    lblResultat.Text = "Chargement...";

    HttpClient client = new HttpClient();
    string ville = txtVille.Text;
    string url = $"https://wttr.in/{ville}?format=j1";

    string response = await client.GetStringAsync(url);
    WeatherResponse? weather = System.Text.Json.JsonSerializer.Deserialize<WeatherResponse>(response);

    if (weather == null || weather.current_condition.Count == 0)
    {
        lblResultat.Text = "Impossible de récupérer la météo.";
        return;
    }

    var current = weather.current_condition[0];
    lblResultat.Text = $"Température : {current.temp_C}°C\nHumidité : {current.humidity}%\nConditions : {current.weatherDesc[0].value}";
}

public class WeatherResponse
{
    public List<CurrentCondition> current_condition { get; set; } = new();
}

public class CurrentCondition
{
    public string temp_C { get; set; } = "";
    public string humidity { get; set; } = "";
    public List<WeatherDesc> weatherDesc { get; set; } = new();
}

public class WeatherDesc
{
    public string value { get; set; } = "";
}   
}
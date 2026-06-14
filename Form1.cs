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
    this.Width = 450;
    this.Height = 350;
    this.BackColor = Color.FromArgb(235, 245, 255);

    Label lblVille = new Label();
    lblVille.Text = "Entrez le nom de la ville :";
    lblVille.Font = new Font("Segoe UI", 11);
    lblVille.Location = new Point(20, 50);
    lblVille.AutoSize = true;
    this.Controls.Add(lblVille);

    txtVille = new TextBox();
    txtVille.Location = new Point(20, 70);
    txtVille.Width = 250;
    txtVille.Font = new Font("Segoe UI", 11);
    this.Controls.Add(txtVille);

    btnRechercher = new Button();
    btnRechercher.Text = "Rechercher";
    btnRechercher.Location = new Point(280, 68);
    btnRechercher.Width = 120;
    btnRechercher.Height = 30;
    btnRechercher.BackColor = Color.FromArgb(70, 130, 220);
    btnRechercher.ForeColor = Color.White;
    btnRechercher.FlatStyle = FlatStyle.Flat;
    this.Controls.Add(btnRechercher);

    lblResultat = new Label();
    lblResultat.Location = new Point(20, 120);
    lblResultat.Width = 380;
    lblResultat.Height = 180;
    lblResultat.Font = new Font("Segoe UI", 12);
    lblResultat.BackColor = Color.White;
    lblResultat.Padding = new Padding(10);
    this.Controls.Add(lblResultat);

        btnRechercher.Click += BtnRechercher_Click;
    }

private string TraduireCondition(string condition)
{
    var traductions = new Dictionary<string, string>
    {
        { "Sunny", "Ensoleillé" },
        { "Clear", "Dégagé" },
        { "Partly cloudy", "Partiellement nuageux" },
        { "Cloudy", "Nuageux" },
        { "Overcast", "Couvert" },
        { "Mist", "Brume" },
        { "Patchy rain possible", "Pluie possible" },
        { "Light rain", "Pluie légère" },
        { "Moderate rain", "Pluie modérée" },
        { "Heavy rain", "Forte pluie" },
        { "Light snow", "Neige légère" },
        { "Thunderstorm", "Orage" }
    };

    condition = condition.Trim();

    if (traductions.ContainsKey(condition))
        return traductions[condition];

    return condition; // si pas trouvé, on affiche l'anglais
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
    string conditionFr = TraduireCondition(current.weatherDesc[0].value);
    lblResultat.Text = $"Température : {current.temp_C}°C\nHumidité : {current.humidity}%\nConditions : {conditionFr}";
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
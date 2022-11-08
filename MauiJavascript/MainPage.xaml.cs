
namespace MauiJavascript;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
    }

	private async void MauiToWebView(object sender, EventArgs e)
	{
		string mauiEntryTextJsFunction = "dataFromArgumentToHTML('" + mauiEntry.Text + "')";

        await jsExampleWebView.EvaluateJavaScriptAsync(mauiEntryTextJsFunction);
    }

    private async void WebViewToMaui(object sender, EventArgs e)
	{
        var result = await jsExampleWebView.EvaluateJavaScriptAsync("dataFromTextarea()");

        await DisplayAlert("Message inside MAUI", result, "OK");
    }
    private async void JsExampleWebView_NavigatingAsync(object sender, WebNavigatingEventArgs e)
    {
        var urlParts = e.Url.Split(".");
        Console.WriteLine(e.Url);
        if (urlParts[0].ToLower().Contains("runcsharp"))
        {
            Console.WriteLine(urlParts);
            var funcToCall = urlParts[1].Split("?");
            var methodName = funcToCall[0];
            var funcParams = funcToCall[1];

            Console.WriteLine("Calling " + methodName);

            // prevent the navigation to complete
            e.Cancel = true;

            if (methodName.Contains("displayalert")) await DisplayAlert(funcParams);

            // TODO smart parsing and type casting of parameters and then some reflection magic
        }

        // Kudos to Mr W's answer on StackOverflow:
        // https://stackoverflow.com/questions/68848742/is-there-a-way-to-create-bi-directional-communication-between-a-shell-app-and-a
    }
    async Task DisplayAlert(string message)
    {
        await DisplayAlert("DisplayAlert in MAUI C#", message, "OK");
    }
}


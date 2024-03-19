namespace Server;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;
        this.ClientSize = new System.Drawing.Size(600, 400);
        this.Text = "Server";
        
        start.Size = new Size(100, 40);
        start.Location = new Point(20, 20);
        start.Text = "Server Start";
        
        server_state.Size = new Size(100, 40);
        server_state.Location = new Point(140, 30);
        server_state.Text = "Press to Start";

        client_state.Size = new Size(100, 40);
        client_state.Location = new Point(140, 100);
        client_state.Text = "";

        client_list.Size = new Size(300, 360);
        client_list.Location = new Point(280, 20);

        this.Controls.Add(start);
        this.Controls.Add(server_state);
        this.Controls.Add(client_state);
        this.Controls.Add(client_list);
    }

    private Button start = new Button();
    private Label server_state = new Label();
    private Label client_state = new Label();
    private ListBox client_list = new ListBox();

    #endregion
}

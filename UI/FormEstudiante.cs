using GestionEstudiantes.Models;

namespace GestionEstudiantes.UI
{
    public partial class FormEstudiante : Form
    {
        public Estudiante Estudiante { get; private set; }
        private bool _esEdicion;

        private TextBox txtNombre;
        private TextBox txtApellido;
        private NumericUpDown numEdad;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private Button btnGuardar;
        private Button btnCancelar;

        public FormEstudiante(Estudiante estudiante = null)
        {
            _esEdicion = estudiante != null;
            Estudiante = estudiante ?? new Estudiante();
            InitializeComponent();
            
            if (_esEdicion)
            {
                CargarDatos();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario
            this.Text = _esEdicion ? "Editar Estudiante" : "Agregar Estudiante";
            this.Size = new Size(450, 420);
            this.MinimumSize = new Size(450, 420);
            this.MaximumSize = new Size(450, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Panel de encabezado
            var panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(59, 130, 246)
            };

            var lblTitulo = new Label
            {
                Text = _esEdicion ? "Editar Estudiante" : "Nuevo Estudiante",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 18)
            };

            panelHeader.Controls.Add(lblTitulo);

            // Panel de contenido
            var panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 20, 30, 20)
            };

            // Crear campos
            int yPos = 20;
            int labelWidth = 100;
            int textBoxWidth = 260;
            int spacing = 55;

            // Nombre
            var lblNombre = CreateLabel("Nombre:", 20, yPos);
            txtNombre = CreateTextBox(130, yPos - 3, textBoxWidth);

            yPos += spacing;

            // Apellido
            var lblApellido = CreateLabel("Apellido:", 20, yPos);
            txtApellido = CreateTextBox(130, yPos - 3, textBoxWidth);

            yPos += spacing;

            // Edad
            var lblEdad = CreateLabel("Edad:", 20, yPos);
            numEdad = new NumericUpDown
            {
                Location = new Point(130, yPos - 3),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 10F),
                Minimum = 1,
                Maximum = 120,
                Value = 18
            };

            yPos += spacing;

            // Email
            var lblEmail = CreateLabel("Email:", 20, yPos);
            txtEmail = CreateTextBox(130, yPos - 3, textBoxWidth);

            yPos += spacing;

            // Teléfono
            var lblTelefono = CreateLabel("Teléfono:", 20, yPos);
            txtTelefono = CreateTextBox(130, yPos - 3, textBoxWidth);

            panelContenido.Controls.AddRange(new Control[] 
            { 
                lblNombre, txtNombre,
                lblApellido, txtApellido,
                lblEdad, numEdad,
                lblEmail, txtEmail,
                lblTelefono, txtTelefono
            });

            // Panel de botones
            var panelBotones = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(20, 15, 20, 15)
            };

            btnGuardar = new Button
            {
                Text = "Guardar",
                Size = new Size(120, 40),
                Location = new Point(180, 15),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(34, 197, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.SemiBold),
                Cursor = Cursors.Hand
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Size = new Size(120, 40),
                Location = new Point(310, 15),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(107, 114, 128),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.SemiBold),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += BtnCancelar_Click;

            panelBotones.Controls.AddRange(new Control[] { btnGuardar, btnCancelar });

            // Agregar paneles al formulario
            this.Controls.Add(panelContenido);
            this.Controls.Add(panelBotones);
            this.Controls.Add(panelHeader);

            this.ResumeLayout(false);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(55, 65, 81),
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private TextBox CreateTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private void CargarDatos()
        {
            txtNombre.Text = Estudiante.Nombre;
            txtApellido.Text = Estudiante.Apellido;
            numEdad.Value = Estudiante.Edad > 0 ? Estudiante.Edad : 18;
            txtEmail.Text = Estudiante.Email;
            txtTelefono.Text = Estudiante.Telefono;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es requerido.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es requerido.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return;
            }

            // Validar email si se proporciona
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !EsEmailValido(txtEmail.Text))
            {
                MessageBox.Show("El formato del email no es válido.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Crear objeto estudiante
            Estudiante = new Estudiante
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Edad = (int)numEdad.Value,
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim()
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool EsEmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

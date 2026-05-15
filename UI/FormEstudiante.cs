using GestionEstudiantes.Models;

namespace GestionEstudiantes.UI
{
    public partial class FormEstudiante : Form
    {
        public Estudiante Estudiante { get; private set; } = new Estudiante();

        private readonly bool _esEdicion;
        private TextBox txtNombre = null!;
        private TextBox txtApellido = null!;
        private NumericUpDown numEdad = null!;
        private TextBox txtEmail = null!;
        private TextBox txtTelefono = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        private static readonly Color ColorPrimario = Color.FromArgb(29, 78, 216);
        private static readonly Color ColorFondo = Color.FromArgb(248, 250, 252);
        private static readonly Color ColorTexto = Color.FromArgb(30, 41, 59);
        private static readonly Color ColorTextoSuave = Color.FromArgb(100, 116, 139);
        private static readonly Color ColorBorde = Color.FromArgb(203, 213, 225);
        private static readonly Color ColorExito = Color.FromArgb(22, 163, 74);

        public FormEstudiante(Estudiante? estudiante = null)
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
            SuspendLayout();

            Text = _esEdicion ? "Editar estudiante" : "Agregar estudiante";
            ClientSize = new Size(540, 520);
            MinimumSize = new Size(540, 520);
            MaximumSize = new Size(540, 520);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = ColorFondo;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = ColorFondo
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 78));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 82));

            root.Controls.Add(CrearHeader(), 0, 0);
            root.Controls.Add(CrearContenido(), 0, 1);
            root.Controls.Add(CrearBotonera(), 0, 2);

            Controls.Add(root);

            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;

            ResumeLayout(false);
        }

        private Control CrearHeader()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorPrimario,
                Padding = new Padding(28, 14, 28, 12)
            };

            var lblTitulo = new Label
            {
                Text = _esEdicion ? "Editar estudiante" : "Nuevo estudiante",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblSubtitulo = new Label
            {
                Text = "Completa los datos basicos del estudiante",
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(219, 234, 254),
                TextAlign = ContentAlignment.MiddleLeft
            };

            panel.Controls.Add(lblSubtitulo);
            panel.Controls.Add(lblTitulo);

            return panel;
        }

        private Control CrearContenido()
        {
            var scrollPanel = new BlueScrollPanel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorFondo,
                ScrollBarColor = ColorPrimario,
                TrackColor = Color.FromArgb(219, 234, 254),
                ContentHeight = 390
            };

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorFondo,
                Padding = new Padding(30, 26, 42, 22)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 340,
                ColumnCount = 2,
                RowCount = 5,
                BackColor = ColorFondo
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < 5; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            }

            txtNombre = CrearTextBox("Nombre");
            txtApellido = CrearTextBox("Apellido");
            numEdad = CrearEdad();
            txtEmail = CrearTextBox("correo@ejemplo.com");
            txtTelefono = CrearTextBox("Telefono");
            txtTelefono.MaxLength = 20;

            AgregarCampo(layout, 0, "Nombre", txtNombre);
            AgregarCampo(layout, 1, "Apellido", txtApellido);
            AgregarCampo(layout, 2, "Edad", numEdad);
            AgregarCampo(layout, 3, "Email", txtEmail);
            AgregarCampo(layout, 4, "Telefono", txtTelefono);

            panel.Controls.Add(layout);

            scrollPanel.ContentPanel.Controls.Add(panel);

            return scrollPanel;
        }

        private Control CrearBotonera()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(30, 18, 30, 18)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 124));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 12));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 124));

            btnCancelar = CrearBoton("Cancelar", Color.FromArgb(100, 116, 139));
            btnCancelar.Click += BtnCancelar_Click;

            btnGuardar = CrearBoton("Guardar", ColorExito);
            btnGuardar.Click += BtnGuardar_Click;

            layout.Controls.Add(btnCancelar, 1, 0);
            layout.Controls.Add(btnGuardar, 3, 0);
            panel.Controls.Add(layout);

            return panel;
        }

        private static void AgregarCampo(TableLayoutPanel layout, int fila, string etiqueta, Control control)
        {
            var label = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = ColorTexto,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 12, 10)
            };

            control.Margin = new Padding(0, 0, 0, 10);
            layout.Controls.Add(label, 0, fila);
            layout.Controls.Add(control, 1, fila);
        }

        private static TextBox CrearTextBox(string placeholder)
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = placeholder,
                BackColor = Color.White,
                ForeColor = ColorTexto,
                MaxLength = 100
            };
        }

        private static NumericUpDown CrearEdad()
        {
            return new NumericUpDown
            {
                Dock = DockStyle.Left,
                Width = 120,
                Font = new Font("Segoe UI", 10F),
                Minimum = 1,
                Maximum = 120,
                Value = 18,
                BackColor = Color.White,
                ForeColor = ColorTexto
            };
        }

        private static Button CrearBoton(string texto, Color color)
        {
            var boton = new Button
            {
                Text = texto,
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            boton.FlatAppearance.BorderSize = 0;
            boton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(color, 0.15F);
            boton.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(color, 0.08F);

            return boton;
        }

        private void CargarDatos()
        {
            txtNombre.Text = Estudiante.Nombre;
            txtApellido.Text = Estudiante.Apellido;
            numEdad.Value = Estudiante.Edad > 0 ? Estudiante.Edad : 18;
            txtEmail.Text = Estudiante.Email;
            txtTelefono.Text = Estudiante.Telefono;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarValidacion("El nombre es requerido.", txtNombre);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MostrarValidacion("El apellido es requerido.", txtApellido);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !EsEmailValido(txtEmail.Text))
            {
                MostrarValidacion("El formato del email no es valido.", txtEmail);
                return;
            }

            Estudiante = new Estudiante
            {
                Id = Estudiante.Id,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Edad = (int)numEdad.Value,
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                FechaRegistro = Estudiante.FechaRegistro
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private static void MostrarValidacion(string mensaje, Control control)
        {
            MessageBox.Show(mensaje, "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private static bool EsEmailValido(string email)
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

    internal sealed class BlueScrollPanel : UserControl
    {
        private readonly Panel viewport;
        private readonly Panel contentPanel;
        private readonly Panel track;
        private readonly Panel thumb;
        private int scrollOffset;
        private bool draggingThumb;
        private int dragStartY;
        private int dragStartOffset;

        public BlueScrollPanel()
        {
            DoubleBuffered = true;
            TabStop = true;

            viewport = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = false,
                BackColor = Color.Transparent
            };

            contentPanel = new Panel
            {
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };

            track = new Panel
            {
                Dock = DockStyle.Right,
                Width = 14,
                BackColor = Color.FromArgb(219, 234, 254),
                Cursor = Cursors.Hand
            };

            thumb = new Panel
            {
                Width = 8,
                Left = 3,
                BackColor = Color.FromArgb(29, 78, 216),
                Cursor = Cursors.Hand
            };

            viewport.Controls.Add(contentPanel);
            track.Controls.Add(thumb);
            Controls.Add(viewport);
            Controls.Add(track);

            MouseWheel += HandleMouseWheel;
            viewport.MouseWheel += HandleMouseWheel;
            contentPanel.MouseWheel += HandleMouseWheel;
            contentPanel.ControlAdded += (_, e) =>
            {
                if (e.Control != null)
                {
                    RegisterMouseWheel(e.Control);
                }
            };
            track.MouseDown += Track_MouseDown;
            thumb.MouseDown += Thumb_MouseDown;
            thumb.MouseMove += Thumb_MouseMove;
            thumb.MouseUp += Thumb_MouseUp;
        }

        public Panel ContentPanel => contentPanel;

        public int ContentHeight
        {
            get => contentPanel.Height;
            set
            {
                contentPanel.Height = Math.Max(value, 0);
                UpdateScrollLayout();
            }
        }

        public Color ScrollBarColor
        {
            get => thumb.BackColor;
            set => thumb.BackColor = value;
        }

        public Color TrackColor
        {
            get => track.BackColor;
            set => track.BackColor = value;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateScrollLayout();
        }

        private void RegisterMouseWheel(Control control)
        {
            control.MouseWheel += HandleMouseWheel;
            control.ControlAdded += (_, e) =>
            {
                if (e.Control != null)
                {
                    RegisterMouseWheel(e.Control);
                }
            };

            foreach (Control child in control.Controls)
            {
                RegisterMouseWheel(child);
            }
        }

        private void HandleMouseWheel(object? sender, MouseEventArgs e)
        {
            ScrollBy(-e.Delta);
        }

        private void Track_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            if (e.Y < thumb.Top)
            {
                ScrollBy(-viewport.ClientSize.Height);
            }
            else if (e.Y > thumb.Bottom)
            {
                ScrollBy(viewport.ClientSize.Height);
            }
        }

        private void Thumb_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            draggingThumb = true;
            dragStartY = Cursor.Position.Y;
            dragStartOffset = scrollOffset;
            thumb.Capture = true;
        }

        private void Thumb_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!draggingThumb)
            {
                return;
            }

            int maxThumbTop = Math.Max(1, track.ClientSize.Height - thumb.Height);
            int maxScroll = ObtenerScrollMaximo();
            int deltaY = Cursor.Position.Y - dragStartY;
            int nuevoOffset = dragStartOffset + (int)Math.Round(deltaY * (double)maxScroll / maxThumbTop);
            ScrollTo(nuevoOffset);
        }

        private void Thumb_MouseUp(object? sender, MouseEventArgs e)
        {
            draggingThumb = false;
            thumb.Capture = false;
        }

        private void ScrollBy(int delta)
        {
            ScrollTo(scrollOffset + delta);
        }

        private void ScrollTo(int offset)
        {
            int maxScroll = ObtenerScrollMaximo();
            scrollOffset = Math.Clamp(offset, 0, maxScroll);
            contentPanel.Top = -scrollOffset;
            UpdateThumb();
        }

        private int ObtenerScrollMaximo()
        {
            return Math.Max(0, contentPanel.Height - viewport.ClientSize.Height);
        }

        private void UpdateScrollLayout()
        {
            int anchoViewport = Math.Max(0, ClientSize.Width - track.Width);
            contentPanel.Width = anchoViewport;
            track.Visible = contentPanel.Height > viewport.ClientSize.Height;
            ScrollTo(scrollOffset);
        }

        private void UpdateThumb()
        {
            if (!track.Visible)
            {
                thumb.Top = 0;
                thumb.Height = track.ClientSize.Height;
                return;
            }

            int maxScroll = ObtenerScrollMaximo();
            int trackHeight = Math.Max(1, track.ClientSize.Height);
            int thumbHeight = Math.Max(44, (int)Math.Round(trackHeight * (double)viewport.ClientSize.Height / contentPanel.Height));
            thumb.Height = Math.Min(trackHeight, thumbHeight);

            int maxThumbTop = Math.Max(0, trackHeight - thumb.Height);
            thumb.Top = maxScroll == 0 ? 0 : (int)Math.Round(scrollOffset * (double)maxThumbTop / maxScroll);
        }
    }
}

using GestionEstudiantes.Data;
using GestionEstudiantes.Models;
using GestionEstudiantes.Repositories;

namespace GestionEstudiantes.UI
{
    public partial class FormPrincipal : Form
    {
        private EstudianteRepository _repository;
        private DataGridView dgvEstudiantes;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnRefrescar;
        private Label lblTotal;
        private Label lblEstadisticas;
        private Panel panelSuperior;
        private Panel panelInferior;
        private Panel panelLateral;

        public FormPrincipal()
        {
            var context = new EstudianteDbContext();
            _repository = new EstudianteRepository(context);
            InitializeComponent();
            CargarEstudiantes();
            ActualizarEstadisticas();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario principal
            this.Text = "Sistema de Gestión de Estudiantes";
            this.Size = new Size(1100, 700);
            this.MinimumSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Panel Superior (Header)
            panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(59, 130, 246),
                Padding = new Padding(20, 0, 20, 0)
            };

            var lblTitulo = new Label
            {
                Text = "Sistema de Gestión de Estudiantes",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            var lblSubtitulo = new Label
            {
                Text = "Administra la información de los estudiantes de manera eficiente",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(219, 234, 254),
                AutoSize = true,
                Location = new Point(20, 48)
            };

            panelSuperior.Controls.AddRange(new Control[] { lblTitulo, lblSubtitulo });

            // Panel Lateral (Sidebar con botones)
            panelLateral = new Panel
            {
                Dock = DockStyle.Left,
                Width = 220,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            // Estilo de botones
            var buttonStyle = new Action<Button, string, Color>((btn, text, color) =>
            {
                btn.Text = text;
                btn.Size = new Size(190, 45);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = color;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.SemiBold);
                btn.Cursor = Cursors.Hand;
                btn.TextAlign = ContentAlignment.MiddleCenter;
            });

            btnAgregar = new Button();
            buttonStyle(btnAgregar, "Agregar Estudiante", Color.FromArgb(34, 197, 94));
            btnAgregar.Location = new Point(15, 20);
            btnAgregar.Click += BtnAgregar_Click;

            btnEditar = new Button();
            buttonStyle(btnEditar, "Editar Estudiante", Color.FromArgb(59, 130, 246));
            btnEditar.Location = new Point(15, 75);
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = new Button();
            buttonStyle(btnEliminar, "Eliminar Estudiante", Color.FromArgb(239, 68, 68));
            btnEliminar.Location = new Point(15, 130);
            btnEliminar.Click += BtnEliminar_Click;

            btnRefrescar = new Button();
            buttonStyle(btnRefrescar, "Refrescar Lista", Color.FromArgb(107, 114, 128));
            btnRefrescar.Location = new Point(15, 185);
            btnRefrescar.Click += BtnRefrescar_Click;

            // Panel de estadísticas en el lateral
            var panelEstadisticas = new Panel
            {
                Location = new Point(15, 260),
                Size = new Size(190, 180),
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(10)
            };

            var lblEstadisticasTitulo = new Label
            {
                Text = "Estadísticas",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(10, 10),
                AutoSize = true
            };

            lblEstadisticas = new Label
            {
                Text = "Cargando...",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(10, 40),
                Size = new Size(170, 130),
                AutoSize = false
            };

            panelEstadisticas.Controls.AddRange(new Control[] { lblEstadisticasTitulo, lblEstadisticas });

            panelLateral.Controls.AddRange(new Control[] { btnAgregar, btnEditar, btnEliminar, btnRefrescar, panelEstadisticas });

            // Panel Inferior (Barra de búsqueda y total)
            panelInferior = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            lblTotal = new Label
            {
                Text = "Total: 0 estudiantes",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(20, 15)
            };

            panelInferior.Controls.Add(lblTotal);

            // Panel de búsqueda
            var panelBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            var lblBuscar = new Label
            {
                Text = "Buscar:",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(55, 65, 81),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            txtBuscar = new TextBox
            {
                Size = new Size(300, 30),
                Location = new Point(80, 15),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtBuscar.KeyPress += TxtBuscar_KeyPress;

            btnBuscar = new Button
            {
                Text = "Buscar",
                Size = new Size(100, 30),
                Location = new Point(390, 15),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.SemiBold),
                Cursor = Cursors.Hand
            };
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Click += BtnBuscar_Click;

            panelBusqueda.Controls.AddRange(new Control[] { lblBuscar, txtBuscar, btnBuscar });

            // DataGridView principal
            dgvEstudiantes = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 9F)
            };

            // Estilo del encabezado
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(59, 130, 246);
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.SemiBold);
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvEstudiantes.ColumnHeadersHeight = 45;
            dgvEstudiantes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Estilo de las filas
            dgvEstudiantes.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgvEstudiantes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvEstudiantes.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dgvEstudiantes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvEstudiantes.RowTemplate.Height = 40;

            dgvEstudiantes.CellDoubleClick += DgvEstudiantes_CellDoubleClick;

            // Panel contenedor principal
            var panelContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            panelContenedor.Controls.Add(dgvEstudiantes);
            panelContenedor.Controls.Add(panelBusqueda);

            // Agregar todos los paneles al formulario
            this.Controls.Add(panelContenedor);
            this.Controls.Add(panelLateral);
            this.Controls.Add(panelInferior);
            this.Controls.Add(panelSuperior);

            this.ResumeLayout(false);
        }

        private void CargarEstudiantes()
        {
            var estudiantes = _repository.ObtenerTodos();
            dgvEstudiantes.DataSource = null;
            dgvEstudiantes.DataSource = estudiantes;

            // Configurar columnas
            if (dgvEstudiantes.Columns.Count > 0)
            {
                dgvEstudiantes.Columns["Id"].HeaderText = "ID";
                dgvEstudiantes.Columns["Id"].Width = 60;
                dgvEstudiantes.Columns["Nombre"].HeaderText = "Nombre";
                dgvEstudiantes.Columns["Apellido"].HeaderText = "Apellido";
                dgvEstudiantes.Columns["Edad"].HeaderText = "Edad";
                dgvEstudiantes.Columns["Edad"].Width = 80;
                dgvEstudiantes.Columns["Email"].HeaderText = "Correo Electrónico";
                dgvEstudiantes.Columns["Telefono"].HeaderText = "Teléfono";
                dgvEstudiantes.Columns["FechaRegistro"].HeaderText = "Fecha de Registro";
                dgvEstudiantes.Columns["FechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            lblTotal.Text = $"Total: {estudiantes.Count} estudiante(s) registrado(s)";
        }

        private void ActualizarEstadisticas()
        {
            var estudiantes = _repository.ObtenerTodos();
            int total = estudiantes.Count;

            if (total > 0)
            {
                double edadPromedio = estudiantes.Average(e => e.Edad);
                int edadMaxima = estudiantes.Max(e => e.Edad);
                int edadMinima = estudiantes.Min(e => e.Edad);

                lblEstadisticas.Text = $"Total: {total} estudiantes\n\n" +
                                      $"Edad promedio: {edadPromedio:F1} años\n\n" +
                                      $"Edad máxima: {edadMaxima} años\n\n" +
                                      $"Edad mínima: {edadMinima} años";
            }
            else
            {
                lblEstadisticas.Text = "No hay estudiantes\nregistrados.";
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (var form = new FormEstudiante())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (_repository.AgregarEstudiante(form.Estudiante))
                    {
                        CargarEstudiantes();
                        ActualizarEstadisticas();
                        MessageBox.Show("Estudiante agregado exitosamente.", "Éxito", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar el estudiante.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEstudiantes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un estudiante para editar.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var estudiante = (Estudiante)dgvEstudiantes.SelectedRows[0].DataBoundItem;
            
            using (var form = new FormEstudiante(estudiante))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (_repository.ActualizarEstudiante(estudiante.Id, form.Estudiante))
                    {
                        CargarEstudiantes();
                        ActualizarEstadisticas();
                        MessageBox.Show("Estudiante actualizado exitosamente.", "Éxito", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el estudiante.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEstudiantes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un estudiante para eliminar.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var estudiante = (Estudiante)dgvEstudiantes.SelectedRows[0].DataBoundItem;
            
            var result = MessageBox.Show(
                $"¿Está seguro de eliminar al estudiante {estudiante.Nombre} {estudiante.Apellido}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (_repository.EliminarEstudiante(estudiante.Id))
                {
                    CargarEstudiantes();
                    ActualizarEstadisticas();
                    MessageBox.Show("Estudiante eliminado exitosamente.", "Éxito", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al eliminar el estudiante.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarEstudiantes();
            ActualizarEstadisticas();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void TxtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Buscar();
                e.Handled = true;
            }
        }

        private void Buscar()
        {
            string termino = txtBuscar.Text.Trim();
            
            if (string.IsNullOrEmpty(termino))
            {
                CargarEstudiantes();
                return;
            }

            var estudiantes = _repository.BuscarPorNombre(termino);
            dgvEstudiantes.DataSource = null;
            dgvEstudiantes.DataSource = estudiantes;

            if (dgvEstudiantes.Columns.Count > 0)
            {
                dgvEstudiantes.Columns["Id"].HeaderText = "ID";
                dgvEstudiantes.Columns["Id"].Width = 60;
                dgvEstudiantes.Columns["Nombre"].HeaderText = "Nombre";
                dgvEstudiantes.Columns["Apellido"].HeaderText = "Apellido";
                dgvEstudiantes.Columns["Edad"].HeaderText = "Edad";
                dgvEstudiantes.Columns["Edad"].Width = 80;
                dgvEstudiantes.Columns["Email"].HeaderText = "Correo Electrónico";
                dgvEstudiantes.Columns["Telefono"].HeaderText = "Teléfono";
                dgvEstudiantes.Columns["FechaRegistro"].HeaderText = "Fecha de Registro";
                dgvEstudiantes.Columns["FechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            lblTotal.Text = $"Resultados: {estudiantes.Count} estudiante(s) encontrado(s)";
        }

        private void DgvEstudiantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEditar_Click(sender, e);
            }
        }
    }
}

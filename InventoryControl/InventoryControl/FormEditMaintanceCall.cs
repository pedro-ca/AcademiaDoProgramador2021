﻿using InventoryControlModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InventoryControl
{
    public partial class FormEditMaintanceCall : Form
    {
        private int MaintanceCallIndex;

        public FormEditMaintanceCall(MaintenanceCall maintenance, int objIndex)
        {
            InitializeComponent();

            MaintanceCallIndex = objIndex;

            textBoxTitleName.Text = maintenance.TitleName;
            textBoxDescriptionName.Text = maintenance.DescriptioName;
            dateTimePickerOpeningDate.Value = maintenance.OpeningDate;
        }

        public void PopulateComboBox(List<Equipment> ListEquip)
        {
            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = ListEquip;

            comboBoxEquipment.DataSource = bindingSource1.DataSource;

            comboBoxEquipment.DisplayMember = "equipmentName";
            comboBoxEquipment.ValueMember = "equipmentName";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxTitleName.Text) && !string.IsNullOrEmpty(textBoxDescriptionName.Text))
            {
                if (comboBoxEquipment.SelectedValue != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja editar o Chamado de Manutenção?", "Confirmação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            string titleName = textBoxTitleName.Text;
                            string descriptionName = textBoxDescriptionName.Text;
                            Equipment equipment = (Equipment)comboBoxEquipment.SelectedItem;
                            DateTime openingDate = dateTimePickerOpeningDate.Value;

                            MaintenanceCall newMainanceCall = new MaintenanceCall(titleName, descriptionName, equipment, openingDate);

                            FormMain parentForm = (FormMain)this.Owner;
                            parentForm.AddMaintenanceCall(newMainanceCall, MaintanceCallIndex);
                            this.Dispose();
                        }
                        catch (ArgumentException ex)
                        {
                            MessageBox.Show("Argumentos inválidos: \n" + ex.Message, "Exceção ArgumentException Capturada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um Equipamento válido antes de editar o Chamado de Manutenção.\nCaso não exista nenhum, cadastre um Equipamento novo primeiro.", "Dados Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos antes de editar o Chamado de Manutenção.", "Dados inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

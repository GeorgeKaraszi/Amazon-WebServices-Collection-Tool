namespace EconServices
{
    partial class AmazonItemGatheringGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AmazonItemGatheringGUI));
            this.productIdTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.mainLV = new System.Windows.Forms.ListView();
            this.itemId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemCat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemSQLState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nonCatCB = new System.Windows.Forms.CheckBox();
            this.numOfItemsNUD = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numOfItemsNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // productIdTxt
            // 
            this.productIdTxt.Location = new System.Drawing.Point(252, 18);
            this.productIdTxt.Name = "productIdTxt";
            this.productIdTxt.Size = new System.Drawing.Size(100, 20);
            this.productIdTxt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Product ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of similar items";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(369, 58);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 4;
            this.startBtn.Text = "Gather Items";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // mainLV
            // 
            this.mainLV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemId,
            this.itemName,
            this.itemCat,
            this.itemUrl,
            this.itemSQLState});
            this.mainLV.Location = new System.Drawing.Point(12, 123);
            this.mainLV.Name = "mainLV";
            this.mainLV.Size = new System.Drawing.Size(888, 356);
            this.mainLV.TabIndex = 5;
            this.mainLV.UseCompatibleStateImageBehavior = false;
            this.mainLV.View = System.Windows.Forms.View.Details;
            // 
            // itemId
            // 
            this.itemId.Text = "ID";
            this.itemId.Width = 85;
            // 
            // itemName
            // 
            this.itemName.Text = "Name";
            this.itemName.Width = 163;
            // 
            // itemCat
            // 
            this.itemCat.Text = "Category";
            this.itemCat.Width = 143;
            // 
            // itemSQLState
            // 
            this.itemSQLState.Text = "Sent To DB";
            this.itemSQLState.Width = 98;
            // 
            // itemUrl
            // 
            this.itemUrl.Text = "URL";
            this.itemUrl.Width = 200;
            // 
            // nonCatCB
            // 
            this.nonCatCB.AutoSize = true;
            this.nonCatCB.Location = new System.Drawing.Point(581, 20);
            this.nonCatCB.Name = "nonCatCB";
            this.nonCatCB.Size = new System.Drawing.Size(208, 17);
            this.nonCatCB.TabIndex = 6;
            this.nonCatCB.Text = "Gather items only in the same category";
            this.nonCatCB.UseVisualStyleBackColor = true;
            // 
            // numOfItemsNUD
            // 
            this.numOfItemsNUD.Location = new System.Drawing.Point(499, 18);
            this.numOfItemsNUD.Name = "numOfItemsNUD";
            this.numOfItemsNUD.Size = new System.Drawing.Size(76, 20);
            this.numOfItemsNUD.TabIndex = 7;
            // 
            // AmazonItemGatheringGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 491);
            this.Controls.Add(this.numOfItemsNUD);
            this.Controls.Add(this.nonCatCB);
            this.Controls.Add(this.mainLV);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.productIdTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AmazonItemGatheringGUI";
            this.Text = "Amazon Item Catcher";
            this.Load += new System.EventHandler(this.AmazonItemGatheringGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numOfItemsNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox productIdTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ListView mainLV;
        private System.Windows.Forms.ColumnHeader itemId;
        private System.Windows.Forms.ColumnHeader itemName;
        private System.Windows.Forms.ColumnHeader itemCat;
        private System.Windows.Forms.ColumnHeader itemSQLState;
        private System.Windows.Forms.ColumnHeader itemUrl;
        private System.Windows.Forms.CheckBox nonCatCB;
        private System.Windows.Forms.NumericUpDown numOfItemsNUD;
    }
}


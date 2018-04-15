using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace Rummikub
{
    public class Tile
    {
        public int value;
        public Type type;
        public bool partof;
        public PictureBox picture;
        

        public new Type GetType()
        {
            return type;
        }

        public int GetValue()
        {
            return value;
        }

        enum CLUBS
        {
            clubs_1 = 1,
            clubs_2,
            clubs_3,
            clubs_4,
            clubs_5,
            clubs_6,
            clubs_7,
            clubs_8,
            clubs_9,
            clubs_10,
            clubs_11,
            clubs_12,
            clubs_13
        }
        enum DIAMONDS
        {
            diamonds_1 = 1,
            diamonds_2,
            diamonds_3,
            diamonds_4,
            diamonds_5,
            diamonds_6,
            diamonds_7,
            diamonds_8,
            diamonds_9,
            diamonds_10,
            diamonds_11,
            diamonds_12,
            diamonds_13
        }
        enum SPADES
        {
            spades_1 = 1,
            spades_2,
            spades_3,
            spades_4,
            spades_5,
            spades_6,
            spades_7,
            spades_8,
            spades_9,
            spades_10,
            spades_11,
            spades_12,
            spades_13
        }
        enum HEARTS
        {
            hearts_1 = 1,
            hearts_2,
            hearts_3,
            hearts_4,
            hearts_5,
            hearts_6,
            hearts_7,
            hearts_8,
            hearts_9,
            hearts_10,
            hearts_11,
            hearts_12,
            hearts_13
        }

     

        public Tile(Tile c)
        {
            this.value = c.value;
            this.type = c.type;
            this.partof = c.partof;
            this.picture = new PictureBox();
            this.picture.Image = c.picture.Image;

        }
        public Tile(PictureBox p1)
        {
            this.partof = false;
            this.picture = p1;
            this.picture.Image = p1.Image;
            bool found = false;
            PictureBox check = new PictureBox();
            Type[] types = (Type[])Enum.GetValues(typeof(Type));
            foreach (Type type in types)

            {
                /*if (found)
                    break;*/
                System.Diagnostics.Debug.WriteLine(type.ToString());
                for (int i = 1; i <= 13; i++)
                {

                    Tile tile = new Tile(i, type);
                    if(tile.picture.Image.Tag.Equals(this.picture.Image.Tag))
                    {
                        this.value = i;
                        this.type = type;
                        //found = true;
                        break;
                    }
                }
            }


        }
    


        
        public Tile()
        { }

        public Tile(int value, Type type)
        {
            this.partof = false;
            this.value = value;
            this.type = type;
            this.picture = new PictureBox();
            string picname;
            switch (type)
            {
                case Type.CLUBS:
                    picname = Enum.GetName(typeof(CLUBS), value);
                    this.picture.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(picname);
                    picture.Image.Tag = picname;
                    break;
                case Type.DIAMONDS:
                    picname = Enum.GetName(typeof(DIAMONDS), value);
                    this.picture.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(picname);
                    picture.Image.Tag = picname;
                    break;
                case Type.SPADES:
                    picname = Enum.GetName(typeof(SPADES), value);
                    this.picture.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(picname);
                    picture.Image.Tag = picname;
                    break;
                case Type.HEARTS:
                    picname = Enum.GetName(typeof(HEARTS), value);
                    this.picture.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(picname);
                    picture.Image.Tag = picname;
                    break;
            }
           
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Size = new Size(60, 80);
            
        }
        public PictureBox getPicture()
        {
            return this.picture;
        }
        public void Newcard(Tile c)

        {

            this.type = c.type;
            this.value = c.value;
            if (c.getPicture() != null)
                this.getPicture().Image = c.getPicture().Image;
            else
            { this.getPicture().Image = Properties.Resources.red_joker; }
        }

        public void setPicture(PictureBox p1)
        { this.picture = p1; }

        public bool equals_tile (Tile t)
        {
            if (this.type != t.type)
                return false;
            if (this.value != t.value)
                return false;
            return true;
        }

        override
        public String ToString ()
        {
            return this.type.ToString() + this.value;
        }
    }

    
}



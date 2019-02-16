new Vue({
    el: '#app',
    data: () => ({
      api: window.api,
      dialog: false,
      dialogDelete: false,
      itemPosDelete: null,
      snackbar: {
        snackbar: false,
        color: 'info',
        mode: 'vertical',
        timeout: 6000,
        text: ''
      },
      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Brand', value: 'brand' },
        { text: 'Description', value: 'description' },
        { text: 'Model', value: 'model' },
        { text: 'Year', value: 'year' },
        { text: 'Kilometers', value: 'kilometers' },
        { text: 'Price', value: 'price' },
        { text: 'Actions', value: 'name', sortable: false }
      ],
      cars: [],
      editedIndex: -1,
      editedItem: {
        id: 0,
        brand: '',
        description: '',
        model: '',
        year: 0,
        kilometers: 0,
        price: 0
      },
      defaultItem: {
        id: 0,
        brand: '',
        description: '',
        model: '',
        year: 0,
        kilometers: 0,
        price: 0
      }
    }),
  
    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'Add New Car' : 'Edit Car'
      }
    },
  
    watch: {
      dialog (val) {
        val || this.close()
      }
    },
  
    created () {
      this.initialize()
    },
  
    methods: {
      initialize () {
        this.api.getCars().then(({data}) => {
            this.cars = data;
        })
      },

      editItem (item) {
        this.editedIndex = this.cars.indexOf(item);
        this.editedItem = Object.assign({}, item);
        this.dialog = true;
      },
  
      deleteItem (item) {
        this.itemPosDelete = item;
        this.dialogDelete = true;
      },

      onConfirmDeleteItem() {
        this.api.deleteCar(this.itemPosDelete.id).then(() => {
            this.itemPosDelete = null;
            this.dialogDelete = false;
            this.snackbar.snackbar = true;
            this.snackbar.text = "car deleted :)"
            this.initialize();
        })
      },
  
      close () {
        this.dialog = false
        setTimeout(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        }, 300)
      },
  
      save () {
        let action = this.editedIndex > -1 ? this.api.putCar : this.api.postCar;
        action(this.editedItem).then(() => {
            this.snackbar.snackbar = true;
            this.snackbar.text = "car saved :)"
            this.initialize();
            this.close()
        });
      }
    }
  })
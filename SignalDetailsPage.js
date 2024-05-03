import React, { useState, useEffect } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Modal } from 'react-native';

const SignalDetailsPage = () => {
  // Test signal with example values
  const signal = {
    name: 'Bitcoin',
    symbol: 'BTC',
    buyPrice: 50000, // Example buy price
    sellPrice: 60000, // Example sell price
    tradeType: 'Long',
  };

  // State to store the current price, amount in dollars, and calculated quantity
  const [currentPrice, setCurrentPrice] = useState(null);
  const [amount, setAmount] = useState('');
  const [quantity, setQuantity] = useState('');
  const [tradeType, setTradeType] = useState('');
  const [showModal, setShowModal] = useState(false);

  // Binance API configuration
  const binanceApiBaseUrl = 'https://api.binance.com/api/v3';
  const binanceApi = axios.create({
    baseURL: binanceApiBaseUrl,
  });

  // Function to fetch the current price
  const fetchCurrentPrice = async () => {
    try {
      const response = await binanceApi.get(`/ticker/price?symbol=${signal.symbol}USDT`);
      setCurrentPrice(response.data.price);
    } catch (error) {
      console.error('Error fetching current price:', error);
    }
  };

  // Effect to fetch current price when component mounts
  useEffect(() => {
    fetchCurrentPrice();
  }, []);

  // Function to handle amount input change
  const handleAmountChange = (text) => {
    setAmount(text);
  };

  // Function to handle amount input submit
  const handleAmountSubmit = () => {
    if (amount && currentPrice) {
      const calculatedQuantity = parseFloat(amount) / parseFloat(currentPrice);
      setQuantity(calculatedQuantity.toFixed(8)); // Round to 8 decimal places
    }
  };

  // Function to handle trade type selection
  const handleTradeType = (type) => {
    setTradeType(type);
  };

  // Function to handle trade execution
  const handleTrade = () => {
    setShowModal(true);
  };

  // Function to confirm and execute trade
  const confirmTrade = () => {
    // Trade execution logic based on the selected trade type
    setShowModal(false);
  };

  // Render
  return (
    <View style={styles.container}>
      <Text>Name: {signal.name}</Text>
      <Text>Symbol: {signal.symbol}</Text>
      <Text>Trade Type: {signal.tradeType}</Text>
      <Text>Buy Price: ${signal.buyPrice}</Text>
      <Text>Sell Price: ${signal.sellPrice}</Text>
      <Text>Current Price: {currentPrice ? `$${currentPrice}` : 'Loading...'}</Text>
      <TextInput
        style={styles.input}
        placeholder="Amount in USD"
        value={amount}
        onChangeText={handleAmountChange}
        onSubmitEditing={handleAmountSubmit}
        keyboardType="numeric"
      />
      <Text>Quantity: {quantity}</Text>
      <View style={styles.tradeButtonsContainer}>
        <TouchableOpacity
          style={[styles.tradeButton, tradeType === 'Long' && styles.selectedTradeButton]}
          onPress={() => handleTradeType('Long')}
        >
          <Text style={styles.tradeButtonText}>Long</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={[styles.tradeButton, tradeType === 'Short' && styles.selectedTradeButton]}
          onPress={() => handleTradeType('Short')}
        >
          <Text style={styles.tradeButtonText}>Short</Text>
        </TouchableOpacity>
      </View>
      <TouchableOpacity style={styles.executeButton} onPress={handleTrade}>
        <Text style={styles.executeButtonText}>Execute Trade</Text>
      </TouchableOpacity>
      <Modal
        visible={showModal}
        transparent={true}
        animationType="slide"
        onRequestClose={() => setShowModal(false)}
      >
        <View style={styles.modalContainer}>
          <View style={styles.modalContent}>
            <Text style={styles.modalText}>Confirm Trade?</Text>
            <TouchableOpacity style={styles.confirmButton} onPress={confirmTrade}>
              <Text style={styles.confirmButtonText}>Confirm</Text>
            </TouchableOpacity>
            <TouchableOpacity style={styles.cancelButton} onPress={() => setShowModal(false)}>
              <Text style={styles.cancelButtonText}>Cancel</Text>
            </TouchableOpacity>
          </View>
        </View>
      </Modal>
      {/* Additional market information section */}
      {/* For example, real-time market data such as trading volume, order book depth, and market sentiment */}
    </View>
  );
};

// Styles
const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  input: {
    width: '80%',
    height: 40,
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    padding: 10,
    marginVertical: 10,
  },
  tradeButtonsContainer: {
    flexDirection: 'row',
    marginVertical: 10,
  },
  tradeButton: {
    flex: 1,
    height: 50,
    borderRadius: 10,
    alignItems: 'center',
    justifyContent: 'center',
    marginHorizontal: 5,
    borderWidth: 1,
    borderColor: '#ccc',
  },
  selectedTradeButton: {
    backgroundColor: 'lightblue',
  },
  tradeButtonText: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  executeButton: {
    width: 200,
    height: 50,
    borderRadius: 10,
    alignItems: 'center',
    justifyContent: 'center',
    marginVertical: 10,
    backgroundColor: 'green',
  },
  executeButtonText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
  },
  modalContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
  },
  modalContent: {
    backgroundColor: 'white',
    borderRadius: 10,
    padding: 20,
    alignItems: 'center',
  },
  modalText: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 20,
  },
  confirmButton: {
    backgroundColor: 'green',
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 5,
    marginBottom: 10,
  },
  confirmButtonText: {
    color: 'white',
    fontSize: 16,
    fontWeight: 'bold',
  },
  cancelButton: {
    backgroundColor: 'red',
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 5,
  },
  cancelButtonText: {
    color: 'white',
    fontSize: 16,
    fontWeight: 'bold',
  },
});

export default SignalDetailsPage;

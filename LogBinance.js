import React, { useState } from 'react';
import { View, TextInput, Button, StyleSheet } from 'react-native';

const LogBinance = () => {
  const [binanceApi, setBinanceApi] = useState('');
  const [binanceSecret, setBinanceSecret] = useState('');

  const handleLogin = () => {
    console.log('Logging in with:', binanceApi, binanceSecret);
  };

  return (
    <View style={styles.container}>
      <TextInput
        style={styles.input}
        placeholder="Binance Api Key"
        value={binanceApi}
        onChangeText={setBinanceApi}
      />
      <TextInput
        style={styles.input}
        placeholder="Binance Secret Api Key"
        value={binanceSecret}
        onChangeText={setBinanceSecret}
      />
      <Button title="Login" onPress={handleLogin} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 20,
  },
  input: {
    width: '100%',
    marginBottom: 10,
    padding: 10,
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
  },
});

export default LogBinance;

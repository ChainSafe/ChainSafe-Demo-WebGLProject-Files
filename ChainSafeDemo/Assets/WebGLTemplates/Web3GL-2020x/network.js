// Used to set the network: https://chainlist.org/
// Onboard JS chain config objects

// interface Chain {
//   namespace?: 'evm';
//   id: ChainId;
//   rpcUrl: string;
//   label: string;
//   token: TokenSymbol;
//   color?: string;
//   icon?: string;
//   providerConnectionInfo?: ConnectionInfo;
//   publicRpcUrl?: string;
//   blockExplorerUrl?: string;
// }

window.networks = [
  {
    id: 5,
    label: "Ethereum Goerli",
    token: "goETH",
    rpcUrl: `https://goerli.infura.io/v3/287318045c6e455ab34b81d6bcd7a65f`,
  }
]
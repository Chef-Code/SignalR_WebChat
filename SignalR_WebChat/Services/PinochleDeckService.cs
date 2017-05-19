using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PinochleDeck;

namespace SignalR_WebChat.Services
{
    public class PinochleDeckService
    {
        private PinochleDealer _dealer;
        private List<PinochlePlayer> _players;
        private Meld _meld;
        public PinochleDeckService()
        {
            _dealer = new PinochleDealer();
            _players = new List<PinochlePlayer>();
            _meld = new Meld();
        }

        public PinochleDeckService(PinochleDealer Dealer)
        {
            _dealer = Dealer;
        }
        public PinochleDeckService(PinochleDealer Dealer, List<PinochlePlayer> Players, Meld Meld)
        {
            _dealer = Dealer;
            _players = Players;
            _meld = Meld;
        }

    }
}
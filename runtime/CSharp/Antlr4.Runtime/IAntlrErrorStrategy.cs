/*
 * [The "BSD license"]
 *  Copyright (c) 2013 Terence Parr
 *  Copyright (c) 2013 Sam Harwell
 *  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *  1. Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *  2. Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 *  3. The name of the author may not be used to endorse or promote products
 *     derived from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
 *  IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 *  OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 *  IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 *  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 *  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 *  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 *  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 *  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 *  THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Sharpen;

namespace Antlr4.Runtime
{
    /// <summary>
    /// The interface for defining strategies to deal with syntax errors encountered
    /// during a parse by ANTLR-generated parsers.
    /// </summary>
    /// <remarks>
    /// The interface for defining strategies to deal with syntax errors encountered
    /// during a parse by ANTLR-generated parsers. We distinguish between three
    /// different kinds of errors:
    /// <ul>
    /// <li>The parser could not figure out which path to take in the ATN (none of
    /// the available alternatives could possibly match)</li>
    /// <li>The current input does not match what we were looking for</li>
    /// <li>A predicate evaluated to false</li>
    /// </ul>
    /// Implementations of this interface report syntax errors by calling
    /// <see cref="Parser.NotifyErrorListeners(string)">Parser.NotifyErrorListeners(string)
    ///     </see>
    /// .
    /// <p/>
    /// TODO: what to do about lexers
    /// </remarks>
    public interface IAntlrErrorStrategy
    {
        /// <summary>
        /// Reset the error handler state for the specified
        /// <code>recognizer</code>
        /// .
        /// </summary>
        /// <param name="recognizer">the parser instance</param>
        void Reset(Parser recognizer);

        /// <summary>
        /// This method is called when an unexpected symbol is encountered during an
        /// inline match operation, such as
        /// <see cref="Parser.Match(int)">Parser.Match(int)</see>
        /// . If the error
        /// strategy successfully recovers from the match failure, this method
        /// returns the
        /// <see cref="IToken">IToken</see>
        /// instance which should be treated as the
        /// successful result of the match.
        /// <p/>
        /// Note that the calling code will not report an error if this method
        /// returns successfully. The error strategy implementation is responsible
        /// for calling
        /// <see cref="Parser.NotifyErrorListeners(string)">Parser.NotifyErrorListeners(string)
        ///     </see>
        /// as appropriate.
        /// </summary>
        /// <param name="recognizer">the parser instance</param>
        /// <exception cref="RecognitionException">
        /// if the error strategy was not able to
        /// recover from the unexpected input symbol
        /// </exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException"></exception>
        [NotNull]
        IToken RecoverInline(Parser recognizer);

        /// <summary>
        /// This method is called to recover from exception
        /// <code>e</code>
        /// . This method is
        /// called after
        /// <see cref="ReportError(Parser, RecognitionException)">ReportError(Parser, RecognitionException)
        ///     </see>
        /// by the default exception handler
        /// generated for a rule method.
        /// </summary>
        /// <seealso cref="ReportError(Parser, RecognitionException)">ReportError(Parser, RecognitionException)
        ///     </seealso>
        /// <param name="recognizer">the parser instance</param>
        /// <param name="e">the recognition exception to recover from</param>
        /// <exception cref="RecognitionException">
        /// if the error strategy could not recover from
        /// the recognition exception
        /// </exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException"></exception>
        void Recover(Parser recognizer, RecognitionException e);

        /// <summary>
        /// This method provides the error handler with an opportunity to handle
        /// syntactic or semantic errors in the input stream before they result in a
        /// <see cref="RecognitionException">RecognitionException</see>
        /// .
        /// <p/>
        /// The generated code currently contains calls to
        /// <see cref="Sync(Parser)">Sync(Parser)</see>
        /// after
        /// entering the decision state of a closure block (
        /// <code>(...)*</code>
        /// or
        /// <code>(...)+</code>
        /// ).
        /// <p/>
        /// For an implementation based on Jim Idle's "magic sync" mechanism, see
        /// <see cref="DefaultErrorStrategy.Sync(Parser)">DefaultErrorStrategy.Sync(Parser)</see>
        /// .
        /// </summary>
        /// <seealso cref="DefaultErrorStrategy.Sync(Parser)">DefaultErrorStrategy.Sync(Parser)
        ///     </seealso>
        /// <param name="recognizer">the parser instance</param>
        /// <exception cref="RecognitionException">
        /// if an error is detected by the error
        /// strategy but cannot be automatically recovered at the current state in
        /// the parsing process
        /// </exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException"></exception>
        void Sync(Parser recognizer);

        /// <summary>
        /// Tests whether or not
        /// <code>recognizer</code>
        /// is in the process of recovering
        /// from an error. In error recovery mode,
        /// <see cref="Parser.Consume()">Parser.Consume()</see>
        /// adds
        /// symbols to the parse tree by calling
        /// <see cref="ParserRuleContext.AddErrorNode(IToken)">ParserRuleContext.AddErrorNode(IToken)
        ///     </see>
        /// instead of
        /// <see cref="ParserRuleContext.AddChild(IToken)">ParserRuleContext.AddChild(IToken)
        ///     </see>
        /// .
        /// </summary>
        /// <param name="recognizer">the parser instance</param>
        /// <returns>
        /// 
        /// <code>true</code>
        /// if the parser is currently recovering from a parse
        /// error, otherwise
        /// <code>false</code>
        /// </returns>
        bool InErrorRecoveryMode(Parser recognizer);

        /// <summary>
        /// This method is called by when the parser successfully matches an input
        /// symbol.
        /// </summary>
        /// <remarks>
        /// This method is called by when the parser successfully matches an input
        /// symbol.
        /// </remarks>
        /// <param name="recognizer">the parser instance</param>
        void ReportMatch(Parser recognizer);

        /// <summary>
        /// Report any kind of
        /// <see cref="RecognitionException">RecognitionException</see>
        /// . This method is called by
        /// the default exception handler generated for a rule method.
        /// </summary>
        /// <param name="recognizer">the parser instance</param>
        /// <param name="e">the recognition exception to report</param>
        void ReportError(Parser recognizer, RecognitionException e);
    }
}

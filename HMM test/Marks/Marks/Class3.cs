﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accord.Statistics.Distributions
{
    using System;
    using Accord.Statistics.Distributions.Fitting;

    /// <summary>
    ///   Common interface for distributions which can be estimated from data.
    /// </summary>
    /// 
    /// <typeparam name="TObservations">The type of the observations, such as <see cref="System.Double"/>.</typeparam>
    /// <typeparam name="TOptions">The type of the options specifying object.</typeparam>
    /// 
    public interface IFittableDistribution<in TObservations, in TOptions> : IFittableDistribution<TObservations>
        where TOptions : class, IFittingOptions
    {

        /// <summary>
        ///   Fits the underlying distribution to a given set of observations.
        /// </summary>
        /// 
        /// <param name="observations">The array of observations to fit the model against. The array
        ///   elements can be either of type double (for univariate data) or
        ///   type double[] (for multivariate data).</param>
        /// <param name="weights">The weight vector containing the weight for each of the samples.</param>
        /// <param name="options">Optional arguments which may be used during fitting, such
        ///   as regularization constants and additional parameters.</param>
        ///   
        void Fit(TObservations[] observations, double[] weights = null, TOptions options = default(TOptions));

    }

    /// <summary>
    ///   Common interface for distributions which can be estimated from data.
    /// </summary>
    /// 
    /// <typeparam name="TObservations">The type of the observations, such as <see cref="System.Double"/>.</typeparam>
    /// 
    public interface IFittableDistribution<in TObservations> : IDistribution<TObservations>
    {
        /// <summary>
        ///   Fits the underlying distribution to a given set of observations.
        /// </summary>
        /// 
        /// <param name="observations">The array of observations to fit the model against. The array
        ///   elements can be either of type double (for univariate data) or
        ///   type double[] (for multivariate data).</param>
        /// <param name="weights">The weight vector containing the weight for each of the samples.</param>
        /// <param name="options">Optional arguments which may be used during fitting, such
        ///   as regularization constants and additional parameters.</param>
        ///   
        void Fit(TObservations[] observations, double[] weights = null, IFittingOptions options = null);

    }

}
